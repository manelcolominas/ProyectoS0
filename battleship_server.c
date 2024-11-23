#include <string.h>
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>
#include <pthread.h>
#include<unistd.h>
#define MAX_USERS 100

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;
//base de datos
MYSQL *conn;
int err;
MYSQL_RES *resultado;
MYSQL_ROW row;
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

// Lista global de usuarios conectados

void show_connected_users(connected_users_list *list){
	printf("Usuarios conectados: \n");
	char notificacion_conectados[1000] = "9*"; // Inicializa con el prefijo "9*"
	
	for (int i = 0; i < list->num; i++){
		printf("%d. %s\n", i+1, list->connected_users[i].username);
		sprintf(notificacion_conectados, "%s/%s",notificacion_conectados, list->connected_users[i].username);
	}	
	
	//ahora enviaremos la lista a todos los conectados
	printf("lista conectados actual: %s\n", notificacion_conectados);
	
	int j;
	for (j=0; j<list->num; j++)
	{
		write(sockets[j],notificacion_conectados ,strlen(notificacion_conectados));
		//printf("connected users: %s\n",notificacion_conectados);
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
	if (!found) return -1;
	
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
		sprintf(connected_users,"%s/%s",connected_users, list->connected_users[i].username);
		i++;
	}
	sprintf(connected_users, "%s", connected_users);
	num_connected = list->num;
	return num_connected;
}
	

	
void broadcast_message(char* notification, char* username, char* response) {
	pthread_mutex_lock(&mutex); // Bloqueo para evitar problemas de concurrencia
	sprintf(notification,"¡%s se ha conectado!", username);
	sprintf(response, "9*%s/%s\n",username,notification); 
	int j;
	for (j=0; j<num_connected; j++)
	{
		write(sockets[j],notification ,strlen(notification));
		printf("connected users: %s\n",notification);
	}
	
	pthread_mutex_unlock(&mutex); // Desbloqueo
}

int give_me_socket(connected_users_list *list, char username[256]){
	// Retorna la posiciÃ³n en la lista o -1 si no estï¿¡ en la llista.
	int i = 0;
	int socket;
	int found = 0 ;
	while (!found && i < list->num)
	{
		if (strcmp(list->connected_users[i].username ,username)== 0){
			found = 1;
			socket = list->connected_users[i].socket;
			return socket;
		}
		i = i+1;
	}
	if (!found) return -1;
}
///////////////////////////
// codificación missatges de sortida del servidor
/*	1-registro*/
/*	2- login*/
/*	3- lista juegos*/
/*	4- consulta oponente*/
/*	5- mostrar juegos*/
/*	6- mostrar ramking*/
/*	8- desconectar*/
/*	9- notificacion usuarios conectados*/

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
		strcpy(sortida, "2*1/3"); // Invalid username or password
	} 
	else {
		int found = 0;
		for (int i = 0; i < list->num && found == 0; i++) {
			if (strcmp(list->connected_users[i].username, username) == 0) {
				strcpy(sortida, "2*1/2"); // Username already in use
				found = 1;
			}
		}
		if (found == 0) {
			res = mysql_store_result(conn);
			if (res != NULL) {
				row = mysql_fetch_row(res);
				if (row != NULL) {
					add_user(list, username, socket);
					sprintf(sortida, "2*1/1:%s",username);
					//strcpy(sortida, "2*1/1"); // Login successful
				} else {
					strcpy(sortida, "2*1/3"); // Invalid username or password
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
	char query[512];
	MYSQL *conn;
	MYSQL_RES *res;
	// Tokenize the input to extract username, email, and password
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	strcpy(username, p);
	p = strtok(NULL, "/");
	strcpy(email, p);
	p = strtok(NULL, "/");
	strcpy(password, p);
	
	// Connect to MySQL
	conn = mysql_init(NULL);
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		sprintf(respuesta, "Error connecting to MySQL: %s", mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	// Check if the username already exists
	sprintf(query, "SELECT ID FROM users WHERE username = '%s';", username);
	if (mysql_query(conn, query) == 0) {
		res = mysql_store_result(conn);
		if (res && mysql_num_rows(res) > 0) {
			strcpy(respuesta, "1*6/0"); // Username already exists
			mysql_free_result(res);
			mysql_close(conn);
			return;
		}
		if (res) mysql_free_result(res);
	}
	
	// Insert the new user
	sprintf(query, "INSERT INTO users (username, email, password) VALUES ('%s', '%s', '%s');", username, email, password);
	if (mysql_query(conn, query) == 0) {
		strcpy(respuesta, "1*6/1"); // Success
	}
	
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
	// Primero, obtenemos los ids de los jugadores para buscar en la tabla 'games'
	sprintf(query,
			"SELECT id_player_1, id_player_2 "
			"FROM games "
			"WHERE (id_player_1 = (SELECT ID FROM users WHERE username = '%s') "
			"AND id_player_2 = (SELECT ID FROM users WHERE username = '%s')) "
			"OR (id_player_1 = (SELECT ID FROM users WHERE username = '%s') "
			"AND id_player_2 = (SELECT ID FROM users WHERE username = '%s'));",
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

void list_of_games(char entrada[512], char respuesta[512]) {
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
		strcpy(respuesta, "ERROR: Failed to initialize MySQL connection\n");
		return;
	}
	
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		strcpy(respuesta, "ERROR: Failed to connect to database\n");
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
		strcpy(respuesta, "ERROR: Query execution failed\n");
		mysql_close(conn);
		return;
	}
	
	res = mysql_store_result(conn);
	if (res == NULL) {
		strcpy(respuesta, "ERROR: Failed to retrieve result\n");
		mysql_close(conn);
		return;
	}
	
	// Construir la lista de oponentes
	while ((row = mysql_fetch_row(res)) != NULL) {
		char opponent_info[256];
		sprintf(opponent_info,"%s\n", row[0]);
		if (strlen(buffer) + strlen(opponent_info) < sizeof(buffer)) {
			strcat(buffer, opponent_info);
		} else {
			strcpy(respuesta, "ERROR: Buffer overflow detected\n");
			mysql_free_result(res);
			mysql_close(conn);
			return;
		}
	}
	if (strlen(buffer) > 0) {
		strcpy(respuesta, buffer);
	} else {
		strcpy(respuesta, "3/No se encontraron oponentes.\n"); // "No se encontraron oponentes
	}
	
	mysql_free_result(res);
	mysql_close(conn);
}

void show_games(char entrada[512], char sortida[512]) {
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

void show_rankings(char entrada[512], char respuesta[512]) {
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	char buffer[4096];  
	buffer[0] = '\0';   
	
	conn = mysql_init(NULL);
	if (conn == NULL) {
		strcpy(respuesta, "ERROR: Could not initialize MySQL connection.\n");
		return;
	}
	
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0) == NULL) {
		sprintf(respuesta, "ERROR: Could not connect to MySQL: %s\n", mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	const char *query = "SELECT username, total_points FROM users ORDER BY total_points DESC;";
	if (mysql_query(conn, query)) {
		sprintf(respuesta, "ERROR: Query failed: %s\n", mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	res = mysql_store_result(conn);
	if (res == NULL) {
		sprintf(respuesta, "ERROR: Could not retrieve results: %s\n", mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	
	while ((row = mysql_fetch_row(res)) != NULL) {
		char line[256];
		sprintf(line, "%s/%s\n", row[0], row[1]);
		
		if (strlen(buffer) + strlen(line) < sizeof(buffer)) {
			strcat(buffer, line);
		} else {
			strcpy(respuesta, "ERROR: Buffer overflow, data too large.\n");
			mysql_free_result(res);
			mysql_close(conn);
			return;
		}
	}
	
	if (strlen(buffer) == 0) {
		strcpy(respuesta, "No rankings available.\n");
	} else {
		strcpy(respuesta, buffer);
	}
	
	mysql_free_result(res);
	mysql_close(conn);
}

void handle_game_invitation(connected_users_list *list, char entrada[512], char respuesta[512]) {
	char username[100];
	char opponent[100];
	char response[100];
	char data_to_send[100];
	char type_of_message[100];
	
	//string data_to_send = $"{serviceCode}/{type_of_message}/{username}/{opponent}";
	// type_of_message = 1 invitations
	// type_of_message = 2 respostes
	
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	strcpy(type_of_message, p);
	
	int socket_ooponent;
	if(strcmp(type_of_message,"1")==0){
		p = strtok(NULL, "/");
		strcpy(username, p);
		p = strtok(NULL, "/");
		strcpy(opponent, p);
		
		strcpy(data_to_send,"7*/");
		strcat(data_to_send,"1/");
		strcat(data_to_send,username);
/*		socket_ooponent = give_me_socket(list,opponent);*/
/*		write(socket_ooponent,data_to_send ,strlen(data_to_send));*/
	}
	if(strcmp(type_of_message,"2")==0){
		p = strtok(NULL, "/");
		strcpy(response, p);
		p = strtok(NULL, "/");
		strcpy(username, p);
		p = strtok(NULL, "/");
		strcpy(opponent, p);
		strcpy(data_to_send,"7*/");
		strcat(data_to_send,"2/");
		strcat(data_to_send,response);
		strcat(data_to_send,"/");
		strcat(data_to_send,username);
/*		socket_ooponent = give_me_socket(list,opponent);*/
/*		write(socket_ooponent,data_to_send ,strlen(data_to_send));*/
	}
	socket_ooponent = give_me_socket(list,opponent);
	write(socket_ooponent,data_to_send ,strlen(data_to_send));
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
	//char notification_connected_users[512];
	
	while (terminar == 0) {
		ret = read(sock_conn, peticion, sizeof(peticion));
		printf("Recibido\n");
		
		peticion[ret] = '\0';
		char peticionInicial[512];
		strcpy(peticionInicial, peticion);
		
		printf("Petición: %s\n", peticion);
		
		char *p = strtok(peticion, "/");
		int codigo = atoi(p);  
		
		printf("Código: %d\n", codigo);
		
		if (codigo == 1){ //signup
			pthread_mutex_lock( &mutex);
			handle_signup(peticionInicial, respuesta);
			printf("Respuesta: %s\n", respuesta);
			write (sock_conn, respuesta, strlen(respuesta));
			pthread_mutex_unlock( &mutex);
			
		}	
	
		else if (codigo == 2){  // Login
			pthread_mutex_lock( &mutex);
			handle_login(&my_connected_users_list, peticionInicial, respuesta, sock_conn); // Llamada a handle_login
			show_connected_users(&my_connected_users_list);
			printf("Respuesta: %s\n", respuesta);
			write (sock_conn, respuesta, strlen(respuesta));
			pthread_mutex_unlock( &mutex);
			show_connected_users(&my_connected_users_list);
		}	
		
		else if (codigo == 3){ // Listar juegos
			char agregar[10] = "3*\n";
			list_of_games(peticionInicial, respuesta);
			strcat(agregar,respuesta);
			printf("Respuesta: %s\n", agregar);
			write (sock_conn, agregar, strlen(agregar));
		}
		
		else if (codigo == 4){  // Consulta de oponente
			char agregar[10] = "4*\n";
			opponent_query(peticionInicial, respuesta);
			strcat(agregar,respuesta);
			printf("Respuesta: %s\n", agregar);
			write (sock_conn, agregar, strlen(agregar));
		}
		
		else if (codigo == 5){  // Mostrar juegos
			char agregar[10] = "5*\n";
			show_games(peticionInicial, respuesta);
			strcat(agregar,respuesta);
			printf("Respuesta: %s\n", agregar);
			write (sock_conn, agregar, strlen(agregar));
		}
		
		else if(codigo == 6){  // Mostrar rankings
			char agregar[10] = "6*\n";
			show_rankings(peticionInicial, respuesta);
			strcat(agregar,respuesta);
			printf("Respuesta: %s\n", agregar);
			write (sock_conn, agregar, strlen(agregar));
		}
		else if (codigo == 7){
			char agregar[10] = "7*\n";
			pthread_mutex_lock( &mutex);
			handle_game_invitation(&my_connected_users_list,peticionInicial,respuesta);
			pthread_mutex_unlock( &mutex);
		}
		else if (codigo == 8){  // Eliminar a un jugador de la lista de conectados
			pthread_mutex_lock( &mutex);
			delete_user(&my_connected_users_list, peticionInicial);
			show_connected_users(&my_connected_users_list);
			terminar = 1;
			pthread_mutex_unlock( &mutex);
			char agregar[10] = "8*\n";
			write (sock_conn, agregar, strlen(agregar));
		}
		
		else{
			printf("El código %d es desconocido"); 
		}
	}
	//servicio finalizado para este cliente
	close(sock_conn);
}
	
int main(int argc, char *argv[]){
		int sock_conn, sock_listen;
		struct sockaddr_in serv_adr;
		
		
/*		add_user(&my_connected_users_list,"manel007",007);*/
		//add_user(&my_connected_users_list,"esther789",789);
/*		char respuesta[200];*/
/*		char entrada[200]; */
/*		strcpy(entrada,"2/manel007/esther789") ;*/
/*		handle_game_invitation(&my_connected_users_list,entrada,respuesta);*/
		
		// Obrim el socket
		if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
			printf("Error creant socket");
		// Fem el bind al port
		
		
		memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
		serv_adr.sin_family = AF_INET;
		
		// asocia el socket a cualquiera de las IP de la m?quina. 
		//htonl formatea el numero que recibe al formato necesario
		serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
		// establecemos el puerto de escucha
		serv_adr.sin_port = htons(8050);
		if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
			printf ("Error al bind\n");
		
		if (listen(sock_listen, 3) < 0)
			printf("Error en el Listen\n");
		
		pthread_t thread;
		int j;
		for (;;){
			printf ("Escuchando\n");
			
			sock_conn = accept(sock_listen, NULL, NULL);
			printf ("He recibido conexion\n");
			
			sockets[j] = sock_conn;
			//sock_conn es el socket que usaremos para este cliente
			
			// Crear thead y decirle lo que tiene que hacer
			
			pthread_create (&thread, NULL, AtenderCliente,&sockets[j]);
			j++;
		}
}

	
