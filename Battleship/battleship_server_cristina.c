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




connected_users_list  my_connected_users_list;
pthread_mutex_t mutex_connected_list;

void show_connected_users(connected_users_list *list){
	printf("Usuarios conectados: \n");
	
	for (int i = 0; i < list->num; i++){
		printf("%d. %s\n", i+1, list->connected_users[i].username);
	}
}
int add_user (connected_users_list *list, char username[256], int socket ){
	/*pthread_mutex_lock(&mutex_connected_list);*/
	if(list->num == 100){
		// retrona -1 si la llista esta plena
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
	// Retorna la posiciÃ³n en la lista o -1 si no estï¿¡ en la llista.
	int i = 0;
	int found = 0 ;
	while (!found && i < list->num)
	{
		if (strcmp(list->connected_users[i].username ,username)== 0){
			found = 1;
		}
		i = i+1;
	}	
	if (found)
		return i;
	else 
		return -1;
}
	
int delete_user(connected_users_list *list, char entrada[256]){	

	char username[100];
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	if (p != NULL) strcpy(username, p);
	int position = give_me_position(list, username);
	if (position == -1){
		return -1;
	}
	else {
		int i = position;
		while (i< list->num-1){
			strcpy(list->connected_users[i].username,list->connected_users[i+1].username);
			list->connected_users[i] = list->connected_users[i+1];
			i++;
		}
		list->num--;
		return 0;
	}
}

void give_me_connected_users(connected_users_list *lista, char resultado[200]){
	// 3/manel007/hugo123/esther123
	sprintf(resultado,"%d",lista -> num);
	int i=0;
	while (i < lista->num){
		sprintf(resultado,"%s/%s",resultado, lista -> connected_users[i].username);
		i++;
	}
}
	
void opponent_query(char *entrada, int sock_conn) {
	
	// mensaje = $"4/{username}/{opponent}";
	int bytes_received = read(sock_conn, entrada, 512);
	if (bytes_received < 0){
		printf("Error al recibir el mensaje");
	}
	if (bytes_received > 0){
		printf("algu byte llega");
	}
	printf("%s",entrada);
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	char me[100];  // Asignar suficiente espacio para el nombre
	strcpy(me, p);
	
	p = strtok(NULL, "/");
	char opponent[100];
	strcpy(opponent, p);
	
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	int err;
	char consulta[512];  // Consulta SQL
	
	// Inicializar conexión a MySQL
	conn = mysql_init(NULL);
	if (conn == NULL) {
		write(sock_conn, "ERROR: Failed to initialize MySQL\n", 34);
		return;
	}
	
	// Conectarse a la base de datos
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		write(sock_conn, "ERROR: Failed to connect to database\n", 38);
		mysql_close(conn);
		return;
	}
	
	// 1. Consulta para obtener los IDs de los jugadores "me" y "opponent"
	sprintf(consulta, 
			"SELECT u1.ID AS me_id, u2.ID AS opponent_id "
			"FROM users u1, users u2 "
			"WHERE u1.username = '%s' AND u2.username = '%s';", 
			me, opponent);
	
	err = mysql_query(conn, consulta);
	if (err != 0) {
		write(sock_conn, "ERROR: Failed to retrieve player IDs\n", 37);
		mysql_close(conn);
		return;
	}
	
	// Obtener el resultado de la consulta
	res = mysql_store_result(conn);
	if (res == NULL) {
		write(sock_conn, "ERROR: Failed to retrieve result\n", 34);
		mysql_close(conn);
		return;
	}
	
	// Validar si se encontraron los IDs de ambos jugadores
	row = mysql_fetch_row(res);
	if (row == NULL) {
		write(sock_conn, "No se encontraron los jugadores\n", 33);
		mysql_free_result(res);
		mysql_close(conn);
		return;
	}
	
	int me_id = atoi(row[0]);        // ID del jugador "me"
	int opponent_id = atoi(row[1]);  // ID del jugador "opponent"
	mysql_free_result(res);  // Liberar el resultado anterior
	
	// 2. Consulta para obtener las partidas entre ambos jugadores
	sprintf(consulta, 
			"SELECT g.id_game, u1.username AS player1, u2.username AS player2, "
			"g.points_player_1, g.points_player_2, g.start_time, g.end_time "
			"FROM games g "
			"JOIN users u1 ON g.id_player_1 = u1.ID "
			"JOIN users u2 ON g.id_player_2 = u2.ID "
			"WHERE (g.id_player_1 = %d AND g.id_player_2 = %d) "
			"OR (g.id_player_1 = %d AND g.id_player_2 = %d);", 
			me_id, opponent_id, opponent_id, me_id);
	
	err = mysql_query(conn, consulta);
	if (err != 0) {
		write(sock_conn, "ERROR: Failed to perform query\n", 32);
		printf("Error al realizar la consulta: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	// Validar si hay resultados de las partidas
	res = mysql_store_result(conn);
	if (res == NULL) {
		write(sock_conn, "ERROR: Failed to retrieve result\n", 34);
		mysql_close(conn);
		return;
	}
	
	int num_rows = mysql_num_rows(res);
	if (num_rows > 0) {
		char buffer[4096]; // Buffer para manejar la respuesta
		buffer[0] = '\0';  // Inicializar el buffer vacío
		
		// Concatenar los resultados de cada partida en el buffer
		while ((row = mysql_fetch_row(res)) != NULL) {
			char game_info[256];
			sprintf(game_info, sizeof(game_info), 
					 "%s / %s / %s / %s / %s / %s / %s\n",
					 row[0], row[1], row[2], row[3], row[4], row[5], row[6]);
			strcat(buffer, game_info);  // Adjuntar la información del juego al buffer
		}
		
		// Enviar el buffer de vuelta al cliente
		write(sock_conn, buffer, strlen(buffer));
	} else {
		write(sock_conn, "No se encontraron partidas entre los jugadores.\n", 47);
	}
	
	// Liberar recursos
	mysql_free_result(res);
	mysql_close(conn);
}

void list_of_games(char *entrada, int sock_conn) {
	// Parsear el mensaje recibido para obtener el nombre de usuario
	// Se espera que el mensaje sea de la forma "3/{username}"
	char *p = strtok(entrada, "/"); // Ignorar el primer token (comando)
	p = strtok(NULL, "/"); // Obtener el segundo token (username)
	
	char username[100];  // Asignar suficiente espacio para el nombre de usuario
	strcpy(username, p); // Copiar el nombre de usuario a la variable
	
	// Declarar variables para MySQL
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	char query[512];  // Para construir la consulta SQL
	char buffer[4096]; // Buffer para enviar resultados al cliente
	buffer[0] = '\0';  // Inicializar el buffer vacío
	
	// Inicializar la conexión MySQL
	conn = mysql_init(NULL);
	if (conn == NULL) {
		// Enviar mensaje de error al cliente si falla la inicialización de MySQL
		write(sock_conn, "ERROR: Failed to initialize MySQL connection\n", 46);
		return;
	}
	
	// Conectarse a la base de datos
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		// Enviar mensaje de error al cliente si falla la conexión a la base de datos
		write(sock_conn, "ERROR: Failed to connect to database\n", 38);
		mysql_close(conn); // Cerrar la conexión antes de salir
		return;
	}
	
	// Crear la consulta SQL para obtener los oponentes del jugador actual
	sprintf(query, sizeof(query),
			 "SELECT DISTINCT u.username AS oponente "
			 "FROM games g "
			 "JOIN users u ON (u.ID = g.id_player_1 OR u.ID = g.id_player_2) "
			 "WHERE (g.id_player_1 = (SELECT ID FROM users WHERE username = '%s') "
			 "OR g.id_player_2 = (SELECT ID FROM users WHERE username = '%s')) "
			 "AND u.username != '%s';",
			 username, username, username); // Evitar incluir al mismo jugador en los resultados
	
	// Ejecutar la consulta SQL
	if (mysql_query(conn, query)) {
		// Enviar mensaje de error si la consulta falla
		write(sock_conn, "ERROR: Query execution failed\n", 30);
		mysql_close(conn); // Cerrar la conexión antes de salir
		return;
	}
	
	// Almacenar el resultado de la consulta
	res = mysql_store_result(conn);
	if (res == NULL) {
		// Enviar mensaje de error si no se pueden recuperar los resultados
		write(sock_conn, "ERROR: Failed to retrieve result\n", 33);
		mysql_close(conn); // Cerrar la conexión antes de salir
		return;
	}
	
	// Concatenar los nombres de los oponentes en el buffer
	while ((row = mysql_fetch_row(res)) != NULL) {
		char opponent_info[256];
		sprintf(opponent_info, sizeof(opponent_info), "%s\n", row[0]); // Formatear la info del oponente
		strcat(buffer, opponent_info);  // Añadir la información al buffer
	}
	
	// Si se encontraron oponentes, enviar la lista al cliente
	if (strlen(buffer) > 0) {
		write(sock_conn, buffer, strlen(buffer));
	} else {
		// Si no se encontraron oponentes, notificar al cliente
		write(sock_conn, "No se encontraron oponentes.\n", 31);
	}
	
	// Liberar el resultado y cerrar la conexión con la base de datos
	mysql_free_result(res);
	mysql_close(conn);
}

void show_games(char *entrada, int sock_conn) {
	MYSQL *conn;
	MYSQL_RES *res;
	
	// 1. Parsear el mensaje recibido del cliente
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
	
	// 2. Conectar a la base de datos
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
	
	// 3. Consulta para obtener el ID del usuario
	char query1[512];
	sprintf(query1, sizeof(query1), "SELECT ID FROM users WHERE username = '%s'", username);
	
	if (mysql_query(conn, query1)) {
		perror("Failed to execute query1");
		mysql_close(conn);
		return;
	}
	
	// Obtener resultados
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		perror("Failed to store result for query1");
		mysql_close(conn);
		return;
	}
	
	MYSQL_ROW row = mysql_fetch_row(result);
	if (row == NULL) {
		perror("User not found");
		mysql_free_result(result);
		mysql_close(conn);
		return;
	}
	
	int id_user = atoi(row[0]);
	mysql_free_result(result);
	
	// 4. Crear la consulta SQL para obtener las partidas del usuario
	char query2[512];
	sprintf(query2, sizeof(query2),
			 "SELECT id_game, id_player_1, id_player_2, points_player_1, points_player_2, start_time, end_time "
			 "FROM games WHERE (id_player_1 = %d OR id_player_2 = %d)", id_user, id_user);
	
	// 5. Si se especifica una fecha de inicio
	if (start_time[0] != '\0') {
		sprintf(query2 + strlen(query2), sizeof(query2) - strlen(query2), " AND start_time >= '%s'", start_time);
	}
	
	// 6. Si se especifica una fecha de fin
	if (end_time[0] != '\0') {
		sprintf(query2 + strlen(query2), sizeof(query2) - strlen(query2), " AND end_time <= '%s'", end_time);
	}
	
	// 7. Ejecutar la consulta para obtener las partidas
	if (mysql_query(conn, query2)) {
		perror("Failed to execute query2");
		mysql_close(conn);
		return;
	}
	
	result = mysql_store_result(conn);
	if (result == NULL) {
		perror("Failed to store result for query2");
		mysql_close(conn);
		return;
	}
	
	// 8. Preparar la respuesta
	char response[2048] = "Games for the user:\n";
	while ((row = mysql_fetch_row(result)) != NULL) {
		char game_info[256];
		sprintf(game_info, sizeof(game_info), 
				 "ID Game: %s, Player 1: %s, Player 2: %s, Points Player 1: %s, Points Player 2: %s, Start: %s, End: %s\n", 
				 row[0] ? row[0] : "NULL",
				 row[1] ? row[1] : "NULL",
				 row[2] ? row[2] : "NULL",
				 row[3] ? row[3] : "NULL",
				 row[4] ? row[4] : "NULL",
				 row[5] ? row[5] : "NULL",
				 row[6] ? row[6] : "NULL");
		
		strncat(response, game_info, sizeof(response) - strlen(response) - 1);
	}
	
	mysql_free_result(result);
	
	// 9. Enviar la respuesta al cliente
	if (sock_conn >= 0) {  // Verificar que el socket es válido
		ssize_t bytes_sent = send(sock_conn, response, strlen(response), 0);
		if (bytes_sent < 0) {
			perror("Error sending response");
		}
	} else {
		perror("Invalid socket");
	}
	
	// 10. Cerrar la conexión MySQL
	mysql_close(conn);
	
	// 11. Cerrar el socket del cliente
	if (sock_conn >= 0) {
		close(sock_conn);
	}
}


void show_rankings(char *entrada, int sock_conn) {
	MYSQL *conn;          // Puntero para manejar la conexión MySQL
	MYSQL_RES *res;       // Puntero para almacenar los resultados de la consulta
	MYSQL_ROW row;        // Puntero para manejar las filas retornadas
	
	// 1. Inicializar la conexión a MySQL
	conn = mysql_init(NULL);
	if (conn == NULL) {
		perror("mysql_init failed");
		return;
	}
	
	// 2. Conectar a la base de datos
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		perror("mysql_real_connect failed");
		mysql_close(conn);
		return;
	}
	
	// 3. Crear la consulta SQL para obtener el ranking de los usuarios
	const char *query = "SELECT username, total_points FROM users ORDER BY total_points DESC;";
	
	// 4. Ejecutar la consulta SQL
	if (mysql_query(conn, query)) {
		perror("mysql_query failed");
		mysql_close(conn);
		return;
	}
	
	// 5. Almacenar los resultados de la consulta
	res = mysql_store_result(conn);
	if (res == NULL) {
		perror("mysql_store_result failed");
		mysql_close(conn);
		return;
	}
	
	// 6. Preparar el buffer para enviar datos al cliente
	char buffer[1024]; // Buffer para enviar los resultados al cliente
	int total_rows = mysql_num_rows(res); // Obtener el número de filas del resultado
	
	// 7. Verificar si hay filas en el resultado
	if (total_rows > 0) {
		// 8. Iterar a través de cada fila de resultados
		while ((row = mysql_fetch_row(res)) != NULL) {
			// Formatear los datos de cada usuario en el formato "username/total_points"
			sprintf(buffer, sizeof(buffer), "%s/%s\n", row[0], row[1]);
			// Enviar los datos al cliente a través del socket y verificar si la operación fue exitosa
			ssize_t bytes_sent = send(sock_conn, buffer, strlen(buffer), 0);
			if (bytes_sent == -1) {
				perror("Error sending data");
				break;  // Si hay un error al enviar, romper el bucle
			}
		}
	} else {
		// Si no hay resultados, enviar un mensaje indicando que no hay rankings disponibles
		sprintf(buffer, sizeof(buffer), "No rankings available.\n");
		if (send(sock_conn, buffer, strlen(buffer), 0) == -1) {
			perror("Error sending no rankings message");
		}
	}
	
	// 9. Liberar los recursos asociados con el resultado de MySQL
	mysql_free_result(res);
	
	// 10. Cerrar la conexión con MySQL
	mysql_close(conn);
	
	// 11. Verificar el estado del socket antes de cerrarlo
	if (sock_conn >= 0) {
		if (close(sock_conn) == -1) {
			perror("Error closing socket");
		}
	} else {
		perror("Invalid socket descriptor");
	}
}


// Función para manejar el login
void handle_login(char entrada[200], char respuestaFuncion[200], int sock_conn) {
	char username[100];
	char password[100];
	char consulta[512];
	int err;
	
	// Tokenizamos la entrada para obtener el nombre de usuario y la contraseña
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	if (p != NULL) strcpy(username, p);
	p = strtok(NULL, "/");
	if (p != NULL) strcpy(password, p);
	
	MYSQL_RES *resultado;
	MYSQL_ROW row;
	
	// Inicializar conexión a la base de datos
	MYSQL *conn = mysql_init(NULL);
	if (conn == NULL || mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		printf("Error de conexión: %s\n", mysql_error(conn));
		strcpy(respuestaFuncion, "ERROR: Database connection failed\n");
		return; // Retornar si la conexión falla
	}
	
	// Comprobar si el usuario ya está conectado
	int found = 0;
	for (int u = 0; u < my_connected_users_list.num && found == 0; u++) {
		if (strcmp(my_connected_users_list.connected_users[u].username, username) == 0) {
			found = 1;
		}
	}
	
	if (found == 1) {
		strcpy(respuestaFuncion, "1/3/"); // Usuario ya conectado
	} else {
		// Consultar la base de datos para verificar las credenciales
		sprintf(consulta, "SELECT ID FROM users WHERE username ='%s' AND password='%s';", username, password);
		
		err = mysql_query(conn, consulta);
		if (err != 0) {
			printf("Error al consultar datos de la base: %u %s\n", mysql_errno(conn), mysql_error(conn));
			strcpy(respuestaFuncion, "ERROR: Query failed\n");
			mysql_close(conn);
			return; // Retornar en caso de error
		}
		
		resultado = mysql_store_result(conn);
		row = mysql_fetch_row(resultado);
		
		if (row == NULL) {
			strcpy(respuestaFuncion, "1/0"); // Credenciales inválidas
		} else {
			// Aquí se supone que add_user es una función que agrega al usuario a la lista de conectados
			// Asegúrate de implementar la función add_user
			int e = add_user(&my_connected_users_list, username, sock_conn);
			if (e == 0) {
				printf("\n introducido en la lista\n");
			} else if (e == 1) {
				printf("\n error al introducir en la lista\n");
			}
			
			strcpy(respuestaFuncion, "1/1/"); // Login exitoso
			//strcat(respuestaFuncion, row[0]); // Asumimos que row[0] tiene el ID del usuario
		}
	}
	
	// Liberar resultados y cerrar la conexión
	if (resultado != NULL) mysql_free_result(resultado);
	mysql_close(conn);
}


// Función para manejar el registro (signup)
void handle_signup(char entrada[200], char respuestaFuncion[200]){
	char username[100];  // Asignar suficiente espacio para el nombre
	char email[100];     // Asignar suficiente espacio para el email
	char password[100];  // Asignar suficiente espacio para la contraseña
	char consulta[512];
	int err;
	
	// Tokenizamos la entrada para obtener el nombre de usuario, email y contraseña
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	if (p != NULL) strcpy(username, p);
	
	p = strtok(NULL, "/");
	if (p != NULL) strcpy(email, p);
	
	p = strtok(NULL, "/");
	if (p != NULL) strcpy(password, p);
	
	MYSQL *conn = mysql_init(NULL);
	if (conn == NULL || mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		printf("Error de conexión: %s\n", mysql_error(conn));
		strcpy(respuestaFuncion, "ERROR: Database connection failed\n");
		return; // Retornar si la conexión falla
	}
	
	// Verificar si el usuario ya existe
	sprintf(consulta, "SELECT * FROM users WHERE username ='%s';", username);
	
	err = mysql_query(conn, consulta);
	if (err != 0) {
		printf("Error al consultar datos de la base: %u %s\n", mysql_errno(conn), mysql_error(conn));
		strcpy(respuestaFuncion, "ERROR: Query failed\n");
		mysql_close(conn);
		return; // Retornar en caso de error
	}
	
	MYSQL_RES *resultado = mysql_store_result(conn);
	MYSQL_ROW row = mysql_fetch_row(resultado);
	
	if (row == NULL) // Si el usuario no existe
	{
		// Registrar el nuevo usuario
		sprintf(consulta, "INSERT INTO users (username, email, password) VALUES ('%s', '%s', '%s');", username, email, password);
		
		err = mysql_query(conn, consulta);
		if (err != 0) {
			printf("Error al insertar datos: %u %s\n", mysql_errno(conn), mysql_error(conn));
			strcpy(respuestaFuncion, "ERROR: Insert failed\n");
			mysql_close(conn);
			return; // Retornar en caso de error
		}
		
		// Enviar respuesta de éxito
		strcpy(respuestaFuncion, "6/0\n"); // 6/0 indica que el registro fue exitoso
	} else { // Si el usuario ya existe
		strcpy(respuestaFuncion, "6/1\n"); // 6/1 indica que el usuario ya está registrado
	}
	
	// Liberar resultados y cerrar la conexión
	mysql_free_result(resultado);
	mysql_close(conn);
}

void *AtenderCliente(void *socket){
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;
	
	char peticion[512];
	char respuesta[512]; // Agregado: Para almacenar respuestas
	int ret;
	int terminar = 0;
	
	// Entramos en un bucle para atender todas las peticiones de este cliente hasta que se desconecte
	while (terminar == 0) {
		// Recibimos la petición del cliente
		ret = read(sock_conn, peticion, sizeof(peticion) - 1); // Reservar espacio para el fin de cadena
		if (ret < 0) {
			perror("Error al leer del socket");
			terminar = 1; // Marcar para terminar si hay un error
			continue;
		}
		
		// Añadimos el fin de string a la petición
		peticion[ret] = '\0';
		char peticionInicial[512];
		strcpy(peticionInicial, peticion);
		
		printf("Petición: %s\n", peticion);
		
		// Procesamos la petición
		char *p = strtok(peticion, "/");
		int codigo = atoi(p);  // Convertimos el código de la petición a entero
		
		if (codigo != 0) {
			printf("Código: %d\n", codigo);
		}
		
		// Procesamos los códigos de solicitud
		switch (codigo) {
		case 1: // Registro
			pthread_mutex_lock( &mutex);
			handle_signup(peticionInicial, respuesta);
			pthread_mutex_unlock( &mutex);
			break;
		case 2: // Login
			pthread_mutex_lock( &mutex);			
			handle_login(peticionInicial, respuesta, sock_conn);
			pthread_mutex_unlock( &mutex);
			show_connected_users(&my_connected_users_list);
			break;
		case 3: // Listar juegos
			list_of_games(peticionInicial, sock_conn); //
			break;
		case 4: // Consulta de oponente
			opponent_query(peticionInicial, sock_conn); //results vs
			break;
		case 5: // Mostrar juegos
			show_games(peticionInicial, sock_conn);
			break;
		case 6: // Mostrar rankings
			show_rankings(peticionInicial, sock_conn); //ranking
			break;
		case 7: // Mostrar usuarios conectados 
			give_me_connected_users(respuesta,sock_conn);
			break;
		case 8: //Eliminar a un jugador de la lista Conectados
			pthread_mutex_lock( &mutex);
			delete_user(&my_connected_users_list, peticionInicial);
			show_connected_users(& my_connected_users_list);
			pthread_mutex_unlock( &mutex);
			break;
		default:
			printf("Código desconocido: %d\n", codigo);
			break;
		}
		
		// Enviamos la respuesta de vuelta al cliente
		if (write(sock_conn, respuesta, strlen(respuesta)) < 0) {
			perror("Error al enviar respuesta");
			terminar = 1; // Marcar para terminar si hay un error
		}
	}
	
	// Cerramos el socket y terminamos el hilo
	close(sock_conn);
	pthread_exit(NULL);
}

int main() {
	
	conn = mysql_init(NULL);
	if (conn == NULL) {
		fprintf(stderr, "mysql_init() failed\n");
		return EXIT_FAILURE;
	}
	
	// Configuración y creación del socket
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
	serv_addr.sin_addr.s_addr = INADDR_ANY; // Aceptar conexiones desde cualquier dirección
	serv_addr.sin_port = htons(3050); // Puerto en el que escuchamos
	
	// Asignamos el socket a la dirección y puerto
	if (bind(sockfd, (struct sockaddr *)&serv_addr, sizeof(serv_addr)) < 0) {
		perror("Error en bind");
		return EXIT_FAILURE;
	}
	
	// Escuchar conexiones entrantes
	listen(sockfd, 5);
	printf("Servidor en espera de conexiones...\n");
	
	clilen = sizeof(cli_addr);
	while (1) {
		// Aceptar una nueva conexión
		newsockfd = accept(sockfd, (struct sockaddr *)&cli_addr, &clilen);
		if (newsockfd < 0) {
			perror("Error en accept");
			continue; // Continuar esperando nuevas conexiones
		}
		
		// Crear un nuevo hilo para atender al cliente
		pthread_t thread;
		if (pthread_create(&thread, NULL, AtenderCliente, (void *)&newsockfd) != 0) {
			perror("Error creando hilo");
			close(newsockfd);
		} else {
			pthread_detach(thread); // Desprendemos el hilo para que se limpie automáticamente
		}
	}
	
	// Cerramos el socket principal (esto nunca se ejecuta debido al bucle infinito)
	close(sockfd);
	return EXIT_SUCCESS;
}
