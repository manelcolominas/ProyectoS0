#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <mysql/mysql.h>  // Para la base de datos MySQL
#include <pthread.h>      // Para el uso de threads
#include <unistd.h>      // Para read, write, close

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

MYSQL *conn;

typedef struct {
	char username [256];
	int socket;
} Connected;

typedef struct {
	Connected connected_users [100];
	int num;
} connected_users_list;

struct ThreadArgs {
	int newsockfd;
	connected_users_list *connected_users_list;
};

// Lista global de usuarios conectados

int add_user (connected_users_list *list, char username[256], int socket ){
	if(list->num >= 100){
		return -1;
	}
	else {
		strcpy(list->connected_users[list->num].username, username);
		list->connected_users[list->num].socket = socket;
		list->num++;
		return 0;
	}
}

int give_me_position(connected_users_list *list, char username[256]){
	// Retorna la posición en la lista o -1 si no est￡ en la llista.
	int i = 0;
	int found = 0 ;
	while (!found && i < list->num)
	{
		if (strcmp(list->connected_users[i].username ,username)== 0){
			found = 1;
			return i;
		}
		i = i+1;
	}
	if (found == 0){ 
		return -1;
	}
}

int delete_user(connected_users_list *list, char username[256]){
	int position;
	int i =0;
	position = give_me_position(list, username);
	if (position == -1){
		return -1;
		}
	else {
		while (i< list->num-1){
			strcpy(list->connected_users[i].username,list->connected_users[i+1].username);
			list->connected_users[i].socket = list->connected_users[i+1].socket;
			i++;
			}
			list->num--;
			return 0;
			}
}

void give_me_connected_users(connected_users_list *list,char connected_users[300]){
	// 3/manel007/hugo123/esther123
	sprintf(connected_users,"%d",list->num);
	int i=0;
	while (i < list->num){
		sprintf(connected_users,"%s/",connected_users, list->connected_users[i].username);
		i++;
	}
}

void notify_all_users(connected_users_list *list, char *notificacion) {
    pthread_mutex_lock(&mutex);
    for (int i = 0; i < list->num; i++) {
        int user_socket = list->connected_users[i].socket;
        if (write(user_socket, notificacion, strlen(notificacion)) < 0) {
            perror("Error al enviar notificación");
        }
    }
    pthread_mutex_unlock(&mutex);
}

///////////////////////////


void handle_login(connected_users_list *list,char *entrada,char *sortida, int socket) {
    char username[100];
    char password[100];
    char query[512];
    int err;

    // Tokenize entrada to obtain username and password
    char *p = strtok(entrada, "/");
    p = strtok(NULL, "/");
    strcpy(username, p);
    p = strtok(NULL, "/");
    strcpy(password, p);

    MYSQL *conn;
    MYSQL_RES *res;
    MYSQL_ROW row;

    // Initialize connection to the database
    conn = mysql_init(NULL);
    if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
        sprintf(sortida, "Error connecting to MySQL server: %s", mysql_error(conn));
        mysql_close(conn);
        return;
    }

    // Check if the user is already connected
    int found = 0;
    for (int i = 0; i < list->num; i++) {
        if (strcmp(list->connected_users[i].username, username) == 0) {
            found = 1;
            sprintf(sortida,"1/User already connected");
            mysql_close(conn);
            return;
        }
    }

    if (!found) {
        // Query the database to verify credentials
        sprintf(query, "SELECT id_user FROM users WHERE username='%s' AND password='%s';", username, password);

        err = mysql_query(conn, query);
        if (err != 0) {
            sprintf(sortida, "Error querying MySQL server: %s", mysql_error(conn));
            mysql_close(conn);
            return;
        }

        res = mysql_store_result(conn);
        row = mysql_fetch_row(res);
        if (row != NULL) {
            // Assuming add_user adds the user to the list of connected users
            add_user(list, username, socket);
            strcpy(sortida, "1/Login successful/"); // Login successful
            strcat(sortida, row[0]); // Assuming row[0] contains the user ID
			} 
		else {
            strcpy(sortida, "1/Invalid username or password");
        }
    }

    // Free result and close connection
	mysql_free_result(res);
    mysql_close(conn);
}

void handle_signup(char *entrada, char *respuesta) {
    
    char username[100];
    char email[100];
    char password[100];
    char query1[512];
    char query2[512];
    
    MYSQL *conn;
    MYSQL_RES *res;
    MYSQL_ROW row;
    int err;
    
    // Tokenize the input to get username, email, and password
    char *p = strtok(entrada, "/");
    strcpy(username, p);
    
    p = strtok(NULL, "/");
    strcpy(email, p);
    
    p = strtok(NULL, "/");
    strcpy(password, p);
    
    // Initialize MySQL connection
    conn = mysql_init(NULL);
    
    // Connect to the MySQL database
    if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
        sprintf(respuesta, "Error connecting to MySQL server: %s", mysql_error(conn));
        mysql_close(conn);
        return;
    }
    
    // Check if the username already exists
    sprintf(query1, "SELECT username FROM users WHERE username = '%s';", username);
    if (mysql_query(conn, query1) != 0) {
        sprintf(respuesta, "Error executing query: %s", mysql_error(conn));
        mysql_close(conn);
        return;
    }
    
    res = mysql_store_result(conn);
    
    if ((row = mysql_fetch_row(res)) != NULL) {
        strcpy(respuesta, "Error: Username already exists.");
        mysql_free_result(res);
        mysql_close(conn);
        return;
    }
    mysql_free_result(res);

    // Insert the new user into the database
    sprintf(query2, "INSERT INTO users (username, email, password) VALUES ('%s', '%s', '%s');", username, email, password);
    if (mysql_query(conn, query2) != 0) {
        sprintf(respuesta, "Error inserting user: %s", mysql_error(conn));
        mysql_close(conn);
        return;
    }
    
    strcpy(respuesta, "SUCCESS");
    mysql_close(conn);
}

void opponent_query(char *entrada, char *sortida) {
    // Expected input format: "3/{username}/{opponent}"
    char *p = strtok(entrada, "/");

    // Parse username and opponent
    p = strtok(NULL, "/");
    char username[100];
    strcpy(username, p);

    p = strtok(NULL, "/");
    char opponent[100];
    strcpy(opponent, p);

    MYSQL *conn;
    MYSQL_RES *res;
    MYSQL_ROW row;
    char query2[512];
    char buffer[4096];
    buffer[0] = '\0';

    // Initialize MySQL connection
    conn = mysql_init(NULL);

    if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
        printf("Error connecting to MySQL server: %s\n", mysql_error(conn));
        mysql_close(conn);
        return;
    }

    // Query for user ID of `username`
    char query1[512];
    sprintf(query1, "SELECT ID FROM users WHERE username = '%s'", username);
    if (mysql_query(conn, query1) != 0) {
        printf("Error querying database for username: %u %s\n", mysql_errno(conn), mysql_error(conn));
        mysql_close(conn);
        return;
    }

    res = mysql_store_result(conn);
    if ((row = mysql_fetch_row(res)) == NULL) {
        printf("Username not found.\n");
        mysql_free_result(res);
        mysql_close(conn);
        return;
    }
    int id_user_1 = atoi(row[0]);
    mysql_free_result(res);

    // Query for user ID of `opponent`
    sprintf(query1, "SELECT ID FROM users WHERE username = '%s'", opponent);
    if (mysql_query(conn, query1) != 0) {
        printf("Error querying database for opponent: %u %s\n", mysql_errno(conn), mysql_error(conn));
        mysql_close(conn);
        return;
    }

    res = mysql_store_result(conn);
	row = mysql_fetch_row(res);
    int id_user_2 = atoi(row[0]);
    mysql_free_result(res);

    // Query for game information between the two players
    sprintf(query2,
        "SELECT id_game, id_player_1, id_player_2, points_player_1, points_player_2, start_time, end_time "
        "FROM games WHERE (id_player_1 = %d AND id_player_2 = %d) OR (id_player_1 = %d AND id_player_2 = %d)",
        id_user_1, id_user_2, id_user_2, id_user_1);

    if (mysql_query(conn, query2) != 0) {
        printf("Error querying database for games: %u %s\n", mysql_errno(conn), mysql_error(conn));
        mysql_close(conn);
        return;
    }

    res = mysql_store_result(conn);
    // Process the result set and append to buffer
    char opponent_info[256];
    while ((row = mysql_fetch_row(res)) != NULL) {
        sprintf(opponent_info,"%s/%s/%s/%s/%s/%s/%s\n",row[0], row[1], row[2], row[3], row[4], row[5], row[6]);
		strcat(buffer, opponent_info);
    }
    strcpy(sortida, buffer);

    mysql_free_result(res);
    mysql_close(conn);
}

void list_of_games(char *entrada, char *sortida) {
	// Se espera que el mensaje sea de la forma "3/{username}"
	char *p = strtok(entrada, "/");

	p = strtok(NULL, "/");
	char username[100];
	strcpy(username, p);

	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	char query2[512];
	char buffer[4096];
	buffer[0] = '\0';

	conn = mysql_init(NULL);

	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		printf("Error connecting to MySQL server: %s\n", mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	char query1[512];
	sprintf(query1,"SELECT ID FROM users WHERE username = '%s'", username);

	if (mysql_query(conn, query1)!=0) {
		printf("Error querying database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		return;
	}

	res = mysql_store_result(conn);
	row = mysql_fetch_row(res);

	int id_user = atoi(row[0]);
	mysql_free_result(res);

	sprintf(query2,"SELECT id_game, id_player_1, id_player_2, points_player_1, points_player_2, start_time, end_time" 
	"FROM games WHERE (id_player_1 = %d OR id_player_2 = %d)", id_user, id_user);

	if (mysql_query(conn, query2)!=0) {  // Corrected variable name
		printf("Error querying database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		return;
	}

	res = mysql_store_result(conn);
	char opponent_info[256];
	while ((row = mysql_fetch_row(res)) != NULL) {
		sprintf(opponent_info, "%s\n", row[0]);
		strcat(buffer, opponent_info);
	}
	strcpy(sortida, buffer);

	mysql_free_result(res);
	mysql_close(conn);
}

void show_games(char *entrada, char *sortida) {
	sortida[0]= '\0';
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	char username[100];
	strcpy(username, p);
	p = strtok(NULL, "/");
	char start_time[15];
	strcpy(start_time, p);
	p = strtok(NULL, "/");
	char end_time[15];
	strcpy(end_time, p);
	char buffer[256];

	conn = mysql_init(NULL);

	if (mysql_real_connect(conn,"localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		printf("Error connecting to MySQL server: %s\n", mysql_error(conn));
		mysql_close(conn);
		return;
	}

	char query1[512];
	sprintf(query1,"SELECT ID FROM users WHERE username = '%s'", username);

	if (mysql_query(conn, query1)!=0) {
		printf("Error querying database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		return;
	}

	res = mysql_store_result(conn);
	row = mysql_fetch_row(res);

	int id_user = atoi(row[0]);
	mysql_free_result(res);

	char query2[512];
	sprintf(query2,
	         "SELECT id_game, id_player_1, id_player_2, points_player_1, points_player_2, start_time, end_time "
	         "FROM games WHERE (id_player_1 = %d OR id_player_2 = %d)", id_user, id_user);

	if (start_time[0] != '\0') {
		sprintf(query2," AND start_time >= '%s'", start_time);
	}
	
	if (end_time[0] != '\0') {
		sprintf(query2," AND end_time <= '%s'", end_time);
	}

	if (mysql_query(conn, query2)!=0) {
		printf("Error querying database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		return;
	}

	res = mysql_store_result(conn);
	char game_info[256];
	while ((row = mysql_fetch_row(res)) != NULL) {
		sprintf(game_info,"%s/%s/%s/%s/%s/%s/%s/",row[0],row[1],row[2],row[3],row[4],row[5],row[6]);
		strcat(buffer, game_info);
	}	
	strcpy(sortida, buffer);
	
	mysql_free_result(res);
	mysql_close(conn);
}

void show_rankings(char *entrada, char *sortida) {
	sortida[0] = '\0';
	char query[256];
	
	MYSQL *conn;          // Puntero para manejar la conexi󮠍ySQL
	MYSQL_RES *res;       // Puntero para almacenar los resultados de la consulta
	MYSQL_ROW row;        // Puntero para manejar las filas retornadas
	
	int err;
    conn = mysql_init(NULL);
	
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
        printf("Error connecting to MySQL server: %s\n", mysql_error(conn));
        mysql_close(conn);
		return;
	}
	
	sprintf(query,"SELECT username, total_points FROM users ORDER BY total_points DESC");
	
	err= mysql_query(conn,query);
	if (err!=0){
		printf("Error querying database: %u %s\n",mysql_errno(conn),mysql_errno(conn));
		mysql_close(conn);
		exit (1);
	}
	
	res = mysql_store_result(conn);
	row = mysql_fetch_row(res);
	
	char buffer[1024];
	char game_info[256];
	while ((row = mysql_fetch_row(res)) != NULL) {
		sprintf(game_info,"%s/%s\n", row[0], row[1]);
        strcat(sortida, game_info);
	}
	strcpy(sortida, buffer);
	
	mysql_free_result(res);
	mysql_close(conn);
}

 void *AtenderCliente(void *socket, connected_users_list *list){
	char peticion[512];
	char respuesta[512];
	char notificacion[512];

	MYSQL *conn;
	int sock_conn;
	int *s;	
	int res;
	s = (int *) socket;
	sock_conn = *s;
	pthread_mutex_lock(&mutex);	
	// Obtener el nombre del usuario (puedes cambiar esto según tu lógica)
    char nombre_usuario[256]; 
    // Aquí debes recibir el nombre del usuario, por ejemplo:
    read(sock_conn, nombre_usuario, sizeof(nombre_usuario));
    nombre_usuario[strcspn(nombre_usuario, "\n")] = 0; // Eliminar salto de línea

    // Agregar el usuario a la lista de conectados
    if (add_user(list, nombre_usuario, sock_conn) == 0) {
        // Notificar a otros usuarios sobre la nueva conexión
        snprintf(notificacion, sizeof(notificacion), "%s se ha conectado.", nombre_usuario);
        notify_all_users(list, notificacion); // Implementa esta función para notificar a todos
    } else {
        // Manejar el error si no se puede agregar el usuario
        printf("No se pudo agregar el usuario: %s\n", nombre_usuario);
        close(sock_conn);
        pthread_exit(NULL);
    }
	int terminar = 0;
	
	while (terminar == 0) {
		res = read(sock_conn, peticion, sizeof(peticion));
		peticion[res]='\0';
		char *p =strtok(peticion,"/");
		int codigo = atoi(p);
		char *entrada = strtok(NULL,"/");
		printf(codigo);
		switch (codigo) {
		case 1: // Registro
			handle_signup(entrada, respuesta);
			break;
		case 2: // Login
			handle_login(list,entrada,respuesta,socket);
			break;
		case 3: // Listar juegos
			list_of_games(entrada, respuesta); //
			break;
		case 4: // Consulta de oponente
			opponent_query(entrada, respuesta); //results vs
			break;
		case 5: // Mostrar juegos
			show_games(entrada, respuesta);
			break;
		case 6: // Mostrar rankings
			show_rankings(entrada, respuesta); //ranking
			break;
		case 7: // Mostrar usuarios conectados 
			give_me_connected_users(list,respuesta);
			break;
		default:
			printf("Codigo desconocido: %d\n", codigo);
			break;
		write(sock_conn, respuesta, strlen(respuesta));
	}
	// Al desconectarse, eliminar el usuario de la lista
    delete_user(list, nombre_usuario);
    snprintf(notificacion, sizeof(notificacion), "%s se ha desconectado.", nombre_usuario);
    notify_all_users(list, notificacion); // Notificar a otros usuarios sobre la desconexión

	close(sock_conn);
	pthread_exit(NULL);
	}
}



int main() {
	connected_users_list  my_connected_users_list;
	my_connected_users_list.num = 0;	
	
	add_user (&my_connected_users_list,"manel005",5);
	add_user (&my_connected_users_list,"manel006",13);
	add_user (&my_connected_users_list,"manel008",14);
	
	// Configuraci�n y creaci�n del socket
	int sockfd, newsockfd;
	struct sockaddr_in serv_addr, cli_addr;
	socklen_t clilen;
	
	sockfd = socket(AF_INET, SOCK_STREAM, 0);
	if (sockfd < 0) {
		perror("Error abriendo socket");
		return EXIT_FAILURE;
	}
	
	// Limpiamos la estructura serv_addr y asignamos valores
	memset(&serv_addr, 0, sizeof(serv_addr));
	serv_addr.sin_family = AF_INET;
	serv_addr.sin_addr.s_addr = INADDR_ANY; // Aceptar conexiones desde cualquier direcci�n
	serv_addr.sin_port = htons(8020); // Puerto en el que escuchamos
	
	// Asignamos el socket a la direcci�n y puerto
	if (bind(sockfd, (struct sockaddr *)&serv_addr, sizeof(serv_addr)) < 0) {
		perror("Error en bind");
		close(sockfd);
		return EXIT_FAILURE;
	}
	// Escuchar conexiones entrantes
	listen(sockfd, 5);
	printf("Servidor en espera de conexiones...\n");
	clilen = sizeof(cli_addr);
	while (1) {
		// Aceptar una nueva conexi�n
		newsockfd = accept(sockfd, (struct sockaddr *)&cli_addr, &clilen);
		if (newsockfd < 0) {
			perror("Error en accept");
			continue; // Continuar esperando nuevas conexiones
		}
		// Crear un nuevo hilo para atender al cliente
		struct ThreadArgs *args = malloc(sizeof(struct ThreadArgs));
		
		args->newsockfd = newsockfd;
		args->connected_users_list = &my_connected_users_list;
		pthread_t thread;
		if (pthread_create(&thread, NULL, AtenderCliente, (void *)args) != 0) {
			perror("Error creating thread");
			free(args);
			close(newsockfd);
		} else {
			pthread_detach(thread); // Detach to allow automatic cleanup
		}
	}
	// Cerramos el socket principal (esto nunca se ejecuta debido al bucle infinito)
	close(sockfd);
	return EXIT_SUCCESS;
}
