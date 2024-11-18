 #include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <mysql/mysql.h>  // Para la base de datos MySQL
#include <pthread.h>      // Para el uso de threads
#include <unistd.h>      // Para read, write, close
#define MAX_USERS 100

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

//variables globales
int num_connected;
int sockets[100];


typedef struct {
	char username [256];
	int socket;
} Connected;

typedef struct {
	Connected connected_users [MAX_USERS];
	int num;
} connected_users_list;

connected_users_list my_connected_users_list;
pthread_mutex_t mutex_connected_list;

// Lista global de usuarios conectados

void show_connected_users(connected_users_list *list){
	printf("Usuarios conectados: \n");
	
	for (int i = 0; i < list->num; i++){
		printf("%d. %s\n", i+1, list->connected_users[i].username);
	}		
}
int add_user(connected_users_list *list, char username[200], int socket) {
	if (list->num >= MAX_USERS) {
		// Return -1 if the list is full
		return -1;
	}
	strcpy(list->connected_users[list->num].username, username);
	list->connected_users[list->num].socket = socket;
	list->num++;
	return 0;
}

int give_me_position(connected_users_list *list, char username[256]){
	// Retorna la posiciÃ³n en la lista o -1 si no estï¿¡ en la llista.
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

int delete_user(connected_users_list *list, char entrada[256]){
	int position;
	int i =0;
	char username[100];
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	strcpy(username, p);
	position = give_me_position(list, username);
	if (position == -1){
		return -1;
		}
	else {
		while (position < list->num-1){
			strcpy(list->connected_users[position].username,list->connected_users[position+1].username);
			list->connected_users[position].socket = list->connected_users[position+1].socket;
			position++;
			}
			list->num--;
			return 0;
		}
}

int give_me_connected_users(connected_users_list *list, char connected_users[300]){
	// 3/manel007/hugo123/esther123
	sprintf(connected_users,"%d",list->num);
	int i=0;
	while (i < list->num){
		sprintf(connected_users,"%s/",connected_users, list->connected_users[i].username);
		i++;
	}
	sprintf(connected_users, "%d/%s", 10, connected_users);
	num_connected = list->num;
	return num_connected;
}

///////////////////////////

void handle_login(connected_users_list *list, char entrada[200], char sortida[200], int socket) {
	char username[100];
	char password[100];
	char query[512];
	int err;
	
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
	
	sprintf(query, "SELECT ID FROM users WHERE username = '%s' AND password = '%s';", username, password);
	
	err = mysql_query(conn, query);
	if (err != 0) {
		strcpy(sortida, "1/3"); // Invalid username or password
	} 
	else {
		int found = 0;
		for (int i = 0; i < list->num && found == 0; i++) {
			if (strcmp(list->connected_users[i].username, username) == 0) {
				strcpy(sortida, "1/2"); // Username already in use
				found = 1;
			}
		}
		if (found == 0) {
			res = mysql_store_result(conn);
			if (res != NULL) {
				row = mysql_fetch_row(res);
				if (row != NULL) {
					add_user(list, username, socket);
					strcpy(sortida, "1/1/10"); // Login successful
				} else {
					strcpy(sortida, "1/3"); // Invalid username or password
				}
				mysql_free_result(res);
			}
		}
	}
	mysql_close(conn);
}

void handle_signup(char entrada[200], char *respuesta) {
    
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
    
	// Connect to the database
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		sprintf(respuesta, "Error connecting to MySQL server: %s", mysql_error(conn));
		mysql_close(conn);
		return;
	}

	// Check if username exists
	sprintf(query1, "SELECT username FROM users WHERE username = '%s';", username);
	mysql_query(conn, query1);

	res = mysql_store_result(conn);
	if (res && mysql_fetch_row(res) != NULL) {
		strcpy(respuesta, "6/0"); //Username already exists
		mysql_free_result(res);
		mysql_close(conn);
		return;
	}
	mysql_free_result(res);
	// Insert the new user
	sprintf(query2, "INSERT INTO users (username, email, password) VALUES ('%s', '%s', '%s');", username, email, password);
	if (mysql_query(conn, query2) != 0) {
		sprintf(respuesta, "Error inserting user: %s", mysql_error(conn));
		mysql_close(conn);
		return;
	}
	strcpy(respuesta, "6/1");
	mysql_close(conn);
}
void opponent_query(char entrada[200], char sortida[200]) {
	// mensaje esperado 4/username/opponent
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	char username[100];
	strcpy(username, p);
	
	p = strtok(NULL, "/");
	char opponent[100];
	strcpy(opponent, p);
	
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	char query[512];
	char buffer[4096];
	buffer[0] = '\0';
	
	conn = mysql_init(NULL);
	
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		printf("Error connecting to MySQL server: %s\n", mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	// Primero, obtenemos los ids de los jugadores para buscar en la tabla 'games'
	sprintf(query,
			"SELECT id_player_1, id_player_2 "
			"FROM games "
			"WHERE (id_player_1 IN (SELECT ID FROM users WHERE username = '%s') "
			"OR id_player_2 IN (SELECT ID FROM users WHERE username = '%s')) "
			"AND (id_player_1 IN (SELECT ID FROM users WHERE username = '%s') "
			"OR id_player_2 IN (SELECT ID FROM users WHERE username = '%s'));",
			username, opponent, opponent, username);
	
	if (mysql_query(conn, query) != 0) {
		printf("Error querying database for games: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	res = mysql_store_result(conn);
	if (res == NULL) {
		printf("Error retrieving results: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	while ((row = mysql_fetch_row(res)) != NULL) {
		int player1_id = atoi(row[0]);
		int player2_id = atoi(row[1]);
		
		char player1_name[100];
		sprintf(query, "SELECT username FROM users WHERE ID = %d;", player1_id);
		if (mysql_query(conn, query) != 0) {
			printf("Error querying username for player1: %u %s\n", mysql_errno(conn), mysql_error(conn));
			mysql_free_result(res);
			mysql_close(conn);
			return;
		}
		
		MYSQL_RES *res1 = mysql_store_result(conn);
		if (res1 == NULL) {
			printf("Error retrieving username for player1: %u %s\n", mysql_errno(conn), mysql_error(conn));
			mysql_free_result(res);
			mysql_close(conn);
			return;
		}
		
		MYSQL_ROW row1 = mysql_fetch_row(res1);
		if (row1 != NULL) {
			strcpy(player1_name, row1[0]);
		}
		mysql_free_result(res1);
		
		char player2_name[100];
		sprintf(query, "SELECT username FROM users WHERE ID = %d;", player2_id);
		if (mysql_query(conn, query) != 0) {
			printf("Error querying username for player2: %u %s\n", mysql_errno(conn), mysql_error(conn));
			mysql_free_result(res);
			mysql_close(conn);
			return;
		}
		
		MYSQL_RES *res2 = mysql_store_result(conn);
		if (res2 == NULL) {
			printf("Error retrieving username for player2: %u %s\n", mysql_errno(conn), mysql_error(conn));
			mysql_free_result(res);
			mysql_close(conn);
			return;
		}
		
		MYSQL_ROW row2 = mysql_fetch_row(res2);
		if (row2 != NULL) {
			strcpy(player2_name, row2[0]);
		}
		mysql_free_result(res2);
		
		sprintf(query,
				"SELECT g.id_game, g.points_player_1, g.points_player_2, g.start_time, g.end_time "
				"FROM games g "
				"WHERE g.id_player_1 = %d AND g.id_player_2 = %d "
				"OR g.id_player_1 = %d AND g.id_player_2 = %d;",
				player1_id, player2_id, player2_id, player1_id);
		
		if (mysql_query(conn, query) != 0) {
			printf("Error querying game information: %u %s\n", mysql_errno(conn), mysql_error(conn));
			mysql_free_result(res);
			mysql_close(conn);
			return;
		}
		
		MYSQL_RES *res_game = mysql_store_result(conn);
		if (res_game == NULL) {
			printf("Error retrieving game results: %u %s\n", mysql_errno(conn), mysql_error(conn));
			mysql_free_result(res);
			mysql_close(conn);
			return;
		}
		
		MYSQL_ROW game_row = mysql_fetch_row(res_game);
		if (game_row != NULL) {
			char game_info[256];
			sprintf(game_info, "%s/%s/%s/%s/%s/%s\n",
					game_row[0], player1_name, player2_name, game_row[1], game_row[2], game_row[3], game_row[4]);
			strcat(buffer, game_info);
		}
		mysql_free_result(res_game);
	}
	
	if (strlen(buffer) == 0) {
		strcpy(sortida, "No games found between the players.\n");
	} else {
		strcpy(sortida, buffer);
	}
	
	mysql_free_result(res);
	mysql_close(conn);
}
void list_of_games(char *entrada, int sock_conn) {
	//mensaje esperado 3/username
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	
	char username[100];
	strcpy(username, p);
	
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	char query[512];
	char buffer[4096];
	buffer[0] = '\0';
	
	conn = mysql_init(NULL);
	if (conn == NULL) {
		write(sock_conn, "ERROR: Failed to initialize MySQL connection\n", 46);
		return;
	}
	
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		write(sock_conn, "ERROR: Failed to connect to database\n", 38);
		mysql_close(conn);
		return;
	}
	
	sprintf(query,
			 "SELECT DISTINCT u.username AS oponente "
			 "FROM games g "
			 "JOIN users u ON (u.ID = g.id_player_1 OR u.ID = g.id_player_2) "
			 "WHERE (g.id_player_1 = (SELECT ID FROM users WHERE username = '%s') "
			 "OR g.id_player_2 = (SELECT ID FROM users WHERE username = '%s')) "
			 "AND u.username != '%s';", username, username, username);
	
	if (mysql_query(conn, query)) {
		write(sock_conn, "ERROR: Query execution failed\n", 30);
		mysql_close(conn);
		return;
	}
	
	res = mysql_store_result(conn);
	if (res == NULL) {
		write(sock_conn, "ERROR: Failed to retrieve result\n", 33);
		mysql_close(conn);
		return;
	}
	
	while ((row = mysql_fetch_row(res)) != NULL) {
		char opponent_info[256];
		snprintf(opponent_info, sizeof(opponent_info), "%s\n", row[0]);
		strcat(buffer, opponent_info);
	}
	
	if (strlen(buffer) > 0) {
		write(sock_conn, buffer, strlen(buffer));
	} else {
		write(sock_conn, "No se encontraron oponentes.\n", 31);
	}
	
	mysql_free_result(res);
	mysql_close(conn);
}

void show_games(char entrada[200], char sortida[200]) {
	// 5/username/initialdate/finaldate
	sortida[0] = '\0';
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	char username[100];
	strcpy(username, p);
	
	p = strtok(NULL, "/");
	char start_time[20];
	strcpy(start_time, p);
	
	p = strtok(NULL, "/");
	char end_time[20];
	strcpy(end_time, p);
	
	char buffer[4096];
	buffer[0] = '\0';
	
	conn = mysql_init(NULL);
	
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		printf("Error connecting to MySQL server: %s\n", mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	// obtener el ID del usuario
	char query1[512];
	sprintf(query1, "SELECT ID FROM users WHERE username = '%s'", username);
	
	if (mysql_query(conn, query1) != 0) {
		printf("Error querying database: %u %s\n", mysql_errno(conn), mysql_error(conn));
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
	int id_user = atoi(row[0]);
	mysql_free_result(res);
	
	// consultar partidas en el rango de fechas 
	char query2[1024];
	sprintf(query2,
			"SELECT g.id_game, "
			"(SELECT username FROM users WHERE ID = g.id_player_1) AS player1, "
			"(SELECT username FROM users WHERE ID = g.id_player_2) AS player2, "
			"g.points_player_1, g.points_player_2, g.start_time, g.end_time "
			"FROM games g "
			"WHERE (g.id_player_1 = %d OR g.id_player_2 = %d) "
			"AND g.start_time >= '%s' AND g.end_time <= '%s'",
			id_user, id_user, start_time, end_time);
	
	if (mysql_query(conn, query2) != 0) {
		printf("Error querying database: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	res = mysql_store_result(conn);
	
	// enviar resultados al cliente
	while ((row = mysql_fetch_row(res)) != NULL) {
		char game_info[256];
		sprintf(game_info, "%s/%s/%s/%s/%s/%s/%s\n", row[0], row[1], row[2], row[3], row[4], row[5], row[6]);
		strcat(buffer, game_info);
	}
	
	if (strlen(buffer) == 0) {
		strcpy(sortida, "No games found.\n");
	} else {
		strcpy(sortida, buffer);
	}
	
	mysql_free_result(res);
	mysql_close(conn);
}

void show_rankings(char *entrada, int sock_conn) {
	// No recibe un mensaje, solo es llamada con el código 6.
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	char buffer[4096];
	buffer[0] = '\0';
	
	conn = mysql_init(NULL);
	if (conn == NULL) {
		perror("mysql_init failed");
		return;
	}
	
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		perror("mysql_real_connect failed");
		mysql_close(conn);
		return;
	}
	
	const char *query = "SELECT username, total_points FROM users ORDER BY total_points DESC;";
	if (mysql_query(conn, query)) {
		perror("mysql_query failed");
		mysql_close(conn);
		return;
	}
	
	res = mysql_store_result(conn);
	if (res == NULL) {
		perror("mysql_store_result failed");
		mysql_close(conn);
		return;
	}
	
	while ((row = mysql_fetch_row(res)) != NULL) {
		char line[256];
		sprintf(line, "%s/%s\n", row[0], row[1]);
		strcat(buffer, line);
	}
	
	if (strlen(buffer) == 0) {
		strcpy(buffer, "No rankings available.\n");
	}
	
	send(sock_conn, buffer, strlen(buffer), 0);
	
	mysql_free_result(res);
	mysql_close(conn);
}



void *AtenderCliente(void *socket){
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;
	
	char peticion[512];
	char respuesta[512];
	int ret;
	int terminar = 0;
	char notification_connected_users[512];
	
	while (terminar == 0) {
		ret = read(sock_conn, peticion, sizeof(peticion) - 1);
		if (ret < 0) {
			perror("Error al leer del socket");
			terminar = 1; 
			continue;
		}
		
		peticion[ret] = '\0';
		char peticionInicial[512];
		strcpy(peticionInicial, peticion);
		
		printf("Petición: %s\n", peticion);
		
		char *p = strtok(peticion, "/");
		int codigo = atoi(p);  
		
		if (codigo != 0) {
			printf("Código: %d\n", codigo);
		}
		
		switch (codigo) {
		case 1:  // Registro
			pthread_mutex_lock(&mutex);
			handle_signup(peticionInicial, respuesta);
			pthread_mutex_unlock(&mutex);
			break;
		case 2:  // Login
			pthread_mutex_lock(&mutex);
			handle_login(&my_connected_users_list, peticionInicial, respuesta, sock_conn); // Llamada a handle_login
			show_connected_users(&my_connected_users_list);
			num_connected = give_me_connected_users(&my_connected_users_list, notification_connected_users);
			
			int j;
			for (j=0; j<num_connected; j++)
				write(sockets[j],notification_connected_users,strlen(notification_connected_users)); 
			pthread_mutex_unlock(&mutex);
			
			break;
		case 3:  // Listar juegos
			list_of_games(peticionInicial, sock_conn);
			break;
		case 4:  // Consulta de oponente
			opponent_query(peticionInicial, sock_conn);
			break;
		case 5:  // Mostrar juegos
			show_games(peticionInicial, sock_conn);
			break;
		case 6:  // Mostrar rankings
			show_rankings(peticionInicial, sock_conn);
			break;
/*		case 7:  */// Mostrar usuarios conectados
/*			give_me_connected_users(respuesta, sock_conn);*/
/*			break;*/
		case 8:  // Eliminar a un jugador de la lista de conectados
			pthread_mutex_lock(&mutex);
			delete_user(&my_connected_users_list, peticionInicial);
			show_connected_users(&my_connected_users_list);
			pthread_mutex_unlock(&mutex);
			break;
		default:
			printf("Código desconocido: %d\n", codigo);
			break;
		}
		
		if (write(sock_conn, respuesta, strlen(respuesta)) < 0) {
			perror("Error al enviar respuesta");
			terminar = 1; 
		}
	}
	
	close(sock_conn);
	pthread_exit(NULL);
}
	
int main(int argc, char *argv[]) {
	int sock_conn, sock_listen;
	
	int sockfd, newsockfd;
	struct sockaddr_in serv_addr, cli_addr;
	socklen_t clilen;
	
	sockfd = socket(AF_INET, SOCK_STREAM, 0);
	if (sockfd < 0) {
		perror("Error abriendo socket");
		return EXIT_FAILURE;
	}
	
	memset(&serv_addr, 0, sizeof(serv_addr));
	serv_addr.sin_family = AF_INET;
	serv_addr.sin_addr.s_addr = INADDR_ANY;
	serv_addr.sin_port = htons(8050);
	
	if (bind(sockfd, (struct sockaddr *)&serv_addr, sizeof(serv_addr)) < 0) {
		perror("Error en bind");
		return EXIT_FAILURE;
	}
	
	if(listen(sockfd, 5)<0){
		perror("Error en el Listen");
		return EXIT_FAILURE;
	}
	
	printf("Servidor en espera de conexiones...\n");
	
	int i = 0;
	clilen = sizeof(cli_addr);
	
	//i = 0;
	// Bucle infinito
/*	for (;;){*/
		
/*		sock_conn = accept(sock_listen, NULL, NULL);*/
/*		sockets[i] =sock_conn;*/
		//sock_conn es el socket que usaremos para este cliente
		
		//crear thread y decirle lo que tiene que hacer
/*		pthread_create (&thread, NULL, AtenderCliente, &sockets[i]);*/
/*		i++;*/
/*	}*/
	
	while (1) {
		newsockfd = accept(sockfd, (struct sockaddr *)&cli_addr, &clilen);
		sockets[i] = newsockfd;
		if (newsockfd < 0) {
			perror("Error en accept");
			continue;
		}
		
		pthread_t thread;
		if (pthread_create(&thread, NULL, AtenderCliente, &sockets[i]) != 0) {
			perror("Error creando hilo");
			close(newsockfd);
		} else {
			pthread_detach(thread);
		}
	}
	
	close(sockfd);
	return EXIT_SUCCESS;
}
	
