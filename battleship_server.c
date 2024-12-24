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
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "M8_battleship_database", 0, NULL, 0) == NULL) {
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
void handle_signup(char entrada[200], char *sortida) {
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
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "M8_battleship_database", 0, NULL, 0) == NULL) {
		sprintf(sortida, "Error connecting to MySQL: %s", mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	// Check if the username already exists
	sprintf(query, "SELECT ID FROM users WHERE username = '%s';", username);
	if (mysql_query(conn, query) == 0) {
		res = mysql_store_result(conn);
		if (res && mysql_num_rows(res) > 0) {
			strcpy(sortida, "1*6/0"); // Username already exists
			mysql_free_result(res);
			mysql_close(conn);
			return;
		}
		if (res) mysql_free_result(res);
	}
	
	// Insert the new user
	sprintf(query, "INSERT INTO users (username, email, password) VALUES ('%s', '%s', '%s');", username, email, password);
	if (mysql_query(conn, query) == 0) {
		strcpy(sortida, "1*6/1"); // Success
	}
	
	mysql_close(conn);
}

void opponent_query(char entrada[200], char sortida[200]) {
 //mensaje esperado 4/username/opponent
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

	if (mysql_real_connect(conn, "localhost", "root", "mysql", "M8_battleship_database", 0, NULL, 0) == NULL) {
		printf("Error connecting to MySQL server: %s\n", mysql_error(conn));
		mysql_close(conn);
		return;
	}

	sprintf(query,"SELECT g.id_game, u1.username AS player1_username, u2.username AS player2_username, "
			"g.points_player_1, g.points_player_2, g.start_time, g.end_time "
			"FROM games g "
			"JOIN users u1 ON g.id_player_1 = u1.ID "
			"JOIN users u2 ON g.id_player_2 = u2.ID "
			"WHERE (u1.username = '%s' AND u2.username = '%s') "
			"OR (u1.username = '%s' AND u2.username = '%s');",username, opponent, opponent, username);
	
	mysql_query(conn, query);
	res = mysql_store_result(conn);
	//ID_Game | Player 1 Username | Player 2 Username | Points 1 | Points 2 | Start Time | End Time
	while ((row = mysql_fetch_row(res)) != NULL) {
		sprintf(sortida,"%s/%s/%s/%s/%s/%s/%s",
			   row[0], row[1], row[2], row[3], row[4], row[5], row[6]);
	}

	mysql_free_result(res);
	mysql_close(conn);
}

void list_of_games(char entrada[512], char sortida[512]){
	
	//mensaje esperado 3/username
	char *p = strtok(entrada, "/");
	p = strtok(NULL, "/");
	
	char username[100];
	strcpy(username, p);
	
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	char user[200];
	char query[512];
	char buffer[4096];
	buffer[0] = '\0';
	
	conn = mysql_init(NULL);
	
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "M8_battleship_database", 0, NULL, 0) == NULL) {
		strcpy(sortida, "ERROR: Failed to connect to database\n");
		mysql_close(conn);
		return;
	}

	sprintf(query,
			"SELECT DISTINCT "
			"CASE "
			"WHEN u1.username = '%s' THEN u2.username "
			"ELSE u1.username "
			"END AS opponent_username "
			"FROM games g "
			"JOIN users u1 ON g.id_player_1 = u1.ID "
			"JOIN users u2 ON g.id_player_2 = u2.ID "
			"WHERE u1.username = '%s' OR u2.username = '%s';",
			username, username, username);
	
	mysql_query(conn, query);
	res = mysql_store_result(conn);
	
	while ((row = mysql_fetch_row(res)) != NULL) {
		//printf(row[0],'\n');
		//strcpy(user,row[0]);
		strcat(sortida, "/");
		strcat(sortida,row[0]);
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
	//strcat(start_time," 14:00:00");
	
	p = strtok(NULL, "/");
	char end_time[20];
	strcpy(end_time, p);
	//strcat(end_time," 15:00:00");
	
	char buffer[4096];
	buffer[0] = '\0';
	
	conn = mysql_init(NULL);
	
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "M8_battleship_database", 0, NULL, 0) == NULL) {
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
	row = mysql_fetch_row(res);

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

void show_rankings(char entrada[512], char sortida[512]) {
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	char buffer[4096];  
	buffer[0] = '\0';   
	
	conn = mysql_init(NULL);
	
	if (mysql_real_connect(conn, "localhost", "root", "mysql", "M8_battleship_database", 0, NULL, 0) == NULL) {
		sprintf(sortida, "ERROR: Could not connect to MySQL: %s\n", mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	char query[1024] = "SELECT username, total_points FROM users ORDER BY total_points DESC;";
	mysql_query(conn, query);
	res = mysql_store_result(conn);
	while ((row = mysql_fetch_row(res)) != NULL) {
		strcat(sortida,row[0]);
		strcat(sortida,"/");
		strcat(sortida, row[1]);
		strcat(sortida,"\n");
	}
	mysql_free_result(res);
	mysql_close(conn);
}

void handle_game_invitation(connected_users_list *list, char entrada[512], char sortida[512]) {
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
		
		socket_ooponent = give_me_socket(list,opponent);
		
		if (socket_ooponent == -1 ){
			socket_ooponent = give_me_socket(list,username);
			strcpy(data_to_send,"7*/2/2/");
			strcat(data_to_send,opponent);
		}
		else {
			strcpy(data_to_send,"7*/");
			strcat(data_to_send,"1/");
			strcat(data_to_send,username);
		}
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
		socket_ooponent = give_me_socket(list,opponent);
/*		if(strcmp(response,"1") == 0){*/
/*			MYSQL *conn;*/
/*			MYSQL_RES *res;*/
/*			MYSQL_ROW row;			*/
/*			conn = mysql_init(NULL);*/
			
/*			if (mysql_real_connect(conn, "localhost", "root", "mysql", "M8_battleship_database", 0, NULL, 0) == NULL) {*/
/*				sprintf(sortida, "ERROR: Could not connect to MySQL: %s\n", mysql_error(conn));*/
/*				mysql_close(conn);*/
/*				return;*/
/*			}*/
/*			char query[1024] = "SELECT MAX(g.id_game) FROM games;";*/
/*			mysql_query(conn, query);*/
/*			res = mysql_store_result(conn);*/
/*			row = mysql_fetch_row(res);*/
/*			strcat(data_to_send,"/");*/
/*			int id_game = atoi(row[0])+1;*/
/*			strcat(data_to_send,row[0]);*/
/*		}*/
	}
	write(socket_ooponent,data_to_send ,strlen(data_to_send));
}

void *AtenderCliente(void *socket){
	int sock_conn;
	int *s;
	s = (int *) socket;
	sock_conn = *s;
	char peticion[512];
/*	char peticion[512];*/
	char sortida[512];
	int ret;
	int terminar = 0;
	//char notification_connected_users[512];
	
	while (terminar == 0) {
		ret = read(sock_conn, peticion, sizeof(peticion));
		printf("Recibido\n");
		
		peticion[ret] = '\0';
		//char sortida[512];
		strcpy(sortida,"");
		char peticionInicial[512];
		strcpy(peticionInicial, peticion);
		
		printf("Petición: %s\n", peticion);
		
		char *p = strtok(peticion, "/");
		int codigo = atoi(p);  
		
		printf("Código: %d\n", codigo);
		
		if (codigo == 1){ //signup
			pthread_mutex_lock( &mutex);
			handle_signup(peticionInicial, sortida);
			printf("sortida: %s\n", sortida);
			write (sock_conn, sortida, strlen(sortida));
			pthread_mutex_unlock( &mutex);
			
		}	
	
		else if (codigo == 2){  // Login
			pthread_mutex_lock( &mutex);
			handle_login(&my_connected_users_list, peticionInicial, sortida, sock_conn); // Llamada a handle_login
			show_connected_users(&my_connected_users_list);
			printf("sortida: %s\n", sortida);
			write (sock_conn, sortida, strlen(sortida));
			pthread_mutex_unlock( &mutex);
			show_connected_users(&my_connected_users_list);
		}	
		
		else if (codigo == 3){ // Listar juegos
			//char agregar[10] = "3*\n";
			char agregar[10] = "3*\n" ;
			list_of_games(peticionInicial, sortida);
			strcat(agregar,sortida);
			printf("sortida: %s\n", agregar);
			write (sock_conn, agregar, strlen(agregar));
		}
		
		else if (codigo == 4){  // Consulta de oponente
			char agregar[10] = "4*\n";
			opponent_query(peticionInicial, sortida);
			strcat(agregar,sortida);
			printf("sortida: %s\n", agregar);
			write (sock_conn, agregar, strlen(agregar));
		}
		
		else if (codigo == 5){  // Mostrar juegos
			char agregar[10] = "5*\n";
			show_games(peticionInicial, sortida);
			strcat(agregar,sortida);
			printf("sortida: %s\n", agregar);
			write (sock_conn, agregar, strlen(agregar));
		}
		
		else if(codigo == 6){  // Mostrar rankings
			char agregar[2000] = "6*\n";
			show_rankings(peticionInicial, sortida);
			strcat(agregar,sortida);
			printf("sortida: %s\n", agregar);
			write (sock_conn, agregar, strlen(agregar));
			//write (sock_conn, agregar, sizeof(agregar)-1);
		}
		else if (codigo == 7){
			char agregar[10] = "7*\n";
			pthread_mutex_lock( &mutex);
			handle_game_invitation(&my_connected_users_list,peticionInicial,sortida);
			pthread_mutex_unlock( &mutex);
		}
		else if (codigo == 8){  // Eliminar a un jugador de la lista de conectados
			pthread_mutex_lock( &mutex);
			delete_user(&my_connected_users_list, peticionInicial);
			show_connected_users(&my_connected_users_list);
			//terminar = 1; // ho comenttat per  aue així no perdi la connexió amb l'usuari
			pthread_mutex_unlock( &mutex);
/*			char agregar[10] = "8*\n";*/
/*			write (sock_conn, agregar, strlen(agregar));*/
		}
		
		else{
			printf("El código %d es desconocido",codigo); 
		}
	}
	//servicio finalizado para este cliente
	close(sock_conn);
}
	
int main(int argc, char *argv[]){
		int sock_conn, sock_listen;
		struct sockaddr_in serv_adr;
		
		// Obrim el socket
		if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
			printf("Error creant socket");
		// Fem el bind al port
		
		
		memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
		serv_adr.sin_family = AF_INET;
		
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
			
			pthread_create (&thread, NULL, AtenderCliente,&sockets[j]);
			j++;
		}
}

	
