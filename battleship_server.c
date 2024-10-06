#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <mysql/mysql.h>

#define MAX_BUFFER_SIZE 512

// Prototypes of functions
void handle_signup(int sock_conn, const char *username, const char *email, const char *password);
void handle_login(int sock_conn, const char *username, const char *password);
void list_of_games(int sock_conn, const char *jugador);
void opponent_query(int sock_conn, const char *me, const char *opponent);
void show_games(int sock_conn);
void show_rankings(int sock_conn);

int main(int argc, char *argv[])
{
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	char peticion[MAX_BUFFER_SIZE];
	
	// Initialize socket
	sock_listen = socket(AF_INET, SOCK_STREAM, 0);
	
	// Configure server address
	memset(&serv_adr, 0, sizeof(serv_adr));
	serv_adr.sin_family = AF_INET;
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	serv_adr.sin_port = htons(9080);  // Port 9080
	
	bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr));
	
	listen(sock_listen, 4);
	
	printf("Server listening on port 9080\n");
	
	// Loop to handle connections
	for(int i = 0; i < 10; i++) {
		printf("Waiting for connections...\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf("Connection received\n");
		
		int terminar = 0;
		while (terminar == 0) {
			ssize_t ret = read(sock_conn, peticion, sizeof(peticion) - 1);
			peticion[ret] = '\0';  // Null terminate received string
			
			printf("Request received: '%s'\n", peticion);
			
			// Tokenize the input based on '/'
			char *p = strtok(peticion, "/");
			if (p != NULL) {
				int codigo = atoi(p);  // Operation code
				printf("Extracted code: %d\n", codigo);
				
				char username[50];
				char email[50];
				char password[50];
				
				
				
				// Handle different service codes
				switch (codigo) {
				case 0:  // Termination
					terminar = 1;  // Exit the loop
					break;
					
				case 1:  // Login
					p = strtok(NULL, "/");
					strcpy(username, p);
					p = strtok(NULL, "/");
					strcpy(password, p);
					handle_login(sock_conn, username, password);
					break;
					
				case 2:  // Signup
					p = strtok(NULL, "/");
					strcpy(username, p);
					p = strtok(NULL, "/");
					strcpy(email, p);
					p = strtok(NULL, "/");
					strcpy(password, p);
					handle_signup(sock_conn, username, email, password);
					break;
				
				case 3:  // Listar oponentes con los que he jugado al menos una vez
					list_of_games(sock_conn, username);
					break;
					
				case 4:  // Games with an opponent
					p = strtok(NULL, "/");
					char me[50]; 
					strcpy(me, p);
					p = strtok(NULL, "/");
					char opponent[50]; 
					strcpy(opponent, p);
					
					// Call the opponent_query function and pass the connection
					opponent_query(sock_conn, me, opponent);
					break;
				case 5:
					show_games(sock_conn);
				case 6:
					show_rankings(sock_conn);
				default:
					// Unknown operation code; do nothing
					break;
				}
			}
		}
		
		close(sock_conn);  // Close connection with client
	}
	
	close(sock_listen);  // Close server socket
	return 0;
}

void handle_signup(int sock_conn, const char *username, const char *email, const char *password) {
	MYSQL *conn;
	char query[512];
	
	// Initialize MySQL connection
	conn = mysql_init(NULL);
	mysql_real_connect(conn, "localhost", "user", "password", "battleship_database", 0, NULL, 0);
	
	// Check if the user already exists
	snprintf(query, sizeof(query), "SELECT COUNT(*) FROM users WHERE username = '%s'", username);
	mysql_query(conn, query);
	
	MYSQL_RES *res = mysql_store_result(conn);
	MYSQL_ROW row = mysql_fetch_row(res);
	int user_exists = atoi(row[0]);
	mysql_free_result(res);
	
	if (user_exists > 0) {
		write(sock_conn, "ERROR: Username already taken\n", 31);
		mysql_close(conn);
		return;
	}
	
	// Insert the new user
	snprintf(query, sizeof(query), "INSERT INTO users (username, email, password) VALUES ('%s', '%s', '%s')", username, email, password);
	mysql_query(conn, query);
	write(sock_conn, "SUCCESS\n", 8);
	
	mysql_close(conn);
}

// Function to handle login
void handle_login(int sock_conn, const char *username, const char *password) {
	MYSQL *conn;
	char query[512];
	
	// Initialize MySQL connection
	conn = mysql_init(NULL);
	mysql_real_connect(conn, "localhost", "user", "password", "battleship_database", 0, NULL, 0);
	
	// Verify user credentials
	snprintf(query, sizeof(query), "SELECT COUNT(*) FROM users WHERE username = '%s' AND password = '%s'", username, password);
	mysql_query(conn, query);
	
	MYSQL_RES *res = mysql_store_result(conn);
	MYSQL_ROW row = mysql_fetch_row(res);
	int valid_user = atoi(row[0]);
	mysql_free_result(res);
	
	if (valid_user > 0) {
		write(sock_conn, "SUCCESS", strlen("SUCCESS"));
		
	} 
	else {
		write(sock_conn, "ERROR: Invalid username or password\n", 37);
	}
	
	mysql_close(conn);
}

void list_of_games(int sock_conn, const char *jugador) 
{
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	char query[512];
	char buffer[4096]; // Buffer para enviar resultados al cliente
	buffer[0] = '\0';  // Inicializar el buffer como vacío
	
	// Inicializar la conexión MySQL
	conn = mysql_init(NULL);
	if (conn == NULL) {
		write(sock_conn, "ERROR: Failed to initialize MySQL connection\n", 46);
		return;
	}
	
	// Conectar a la base de datos
	if (mysql_real_connect(conn, "localhost", "user", "password", "battleship_database", 0, NULL, 0) == NULL) {
		write(sock_conn, "ERROR: Failed to connect to database\n", 38);
		mysql_close(conn);
		return;
	}
	
	// Crear la consulta para obtener oponentes
	snprintf(query, sizeof(query),
			 "SELECT u.username AS oponente "
			 "FROM games g "
			 "JOIN users u ON (u.id_user = g.id_player_1 OR u.id_user = g.id_player_2) "
			 "WHERE (g.id_player_1 = (SELECT id_user FROM users WHERE username = '%s') "
			 "OR g.id_player_2 = (SELECT id_user FROM users WHERE username = '%s')) "
			 "AND u.username != '%s';",
			 jugador, jugador, jugador);
	
	// Ejecutar la consulta
	if (mysql_query(conn, query)) {
		write(sock_conn, "ERROR: Query execution failed\n", 30);
		mysql_close(conn);
		return;
	}
	
	// Obtener el resultado de la consulta
	res = mysql_store_result(conn);
	if (res == NULL) {
		write(sock_conn, "ERROR: Failed to retrieve result\n", 33);
		mysql_close(conn);
		return;
	}
	
	// Concatenar los resultados en el buffer
	while ((row = mysql_fetch_row(res)) != NULL) {
		char opponent_info[256];
		snprintf(opponent_info, sizeof(opponent_info), "%s\n", row[0]);
		strcat(buffer, opponent_info);  // Adjuntar la información del oponente al buffer
	}
	
	// Enviar los resultados al cliente
	if (strlen(buffer) > 0) {
		write(sock_conn, buffer, strlen(buffer));
	} else {
		write(sock_conn, "No se encontraron oponentes.\n", 31);
	}
	
	// Liberar el resultado
	mysql_free_result(res);
	mysql_close(conn);
}

void opponent_query(int sock_conn, const char *me, const char *opponent) 
{
	MYSQL *conn;
	MYSQL_RES *res;
	MYSQL_ROW row;
	int err;
	char consulta[512];  // Consulta SQL
	
	// Inicializar la conexión a MySQL
	conn = mysql_init(NULL);
	mysql_real_connect(conn, "localhost", "user", "password", "battleship_database", 0, NULL, 0);
	
	// Crear la consulta SQL
	sprintf(consulta, 
			"SELECT g.id_game, u1.username AS player1, u2.username AS player2, "
			"g.points_player_1, g.points_player_2, g.start_time, g.end_time "
			"FROM games g "
			"JOIN users u1 ON g.id_player_1 = u1.id_user "
			"JOIN users u2 ON g.id_player_2 = u2.id_user "
			"WHERE (u1.username = '%s' AND u2.username = '%s') OR (u1.username = '%s' AND u2.username = '%s');", 
			me, opponent, opponent, me);
	
	err = mysql_query(conn, consulta);
	if (err != 0)
	{
		write(sock_conn, "ERROR: Failed to perform query\n", 32);
		printf("Error al realizar la consulta: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	res = mysql_store_result(conn);
	if (res == NULL)
	{
		write(sock_conn, "ERROR: Failed to retrieve result\n", 34);
		printf("Error al obtener el resultado: %u %s\n", mysql_errno(conn), mysql_error(conn));
		mysql_close(conn);
		return;
	}
	
	int num_rows = mysql_num_rows(res);
	if (num_rows > 0) 
	{
		char buffer[4096]; // Aumentar tamaño del buffer para manejar más datos
		buffer[0] = '\0';  // Inicializar el buffer como vacío
		
		// Concatenar los resultados de cada juego en el buffer
		while ((row = mysql_fetch_row(res)) != NULL)
		{
			char game_info[256];
			snprintf(game_info, sizeof(game_info), 
					 "ID Partida: %s | Jugador 1: %s | Jugador 2: %s | Puntos Jugador 1: %s | Puntos Jugador 2: %s | INICIO: %s | FINAL: %s\n",
					 row[0], row[1], row[2], row[3], row[4], row[5], row[6]);
			strcat(buffer, game_info);  // Adjuntar la información del juego al buffer
		}
		
		// Enviar el buffer de vuelta al cliente
		write(sock_conn, buffer, strlen(buffer));
	} 
	else 
	{
		write(sock_conn, "No se encontraron partidas entre los jugadores.\n", 47);
	}
	
	mysql_free_result(res);
	mysql_close(conn);
}

void show_games(int sock_conn) 
{
	MYSQL *conn; // MySQL connection
    MYSQL_RES *res; // Result set
    char buffer[512]; // Buffer for received data
    char username[256]; // To store the username
    char start_time[256]; // To store the start time
    char end_time[256]; // To store the end time

    // Step 1: Receive a message from the client
    ssize_t bytes_received = recv(sock_conn, buffer, sizeof(buffer) - 1, 0);
    buffer[bytes_received] = '\0'; // Null-terminate the received message

    // Step 2: Parse the received message
    sscanf(buffer, "5/%[^/]/%[^/]/%[^/]", username, start_time, end_time);

    // Step 3: Find id_user based on username
    char query1[512];
    snprintf(query1, sizeof(query1), 
             "SELECT id_user FROM users WHERE username = '%s'", username);
    mysql_query(conn, query1);

    MYSQL_RES *result = mysql_store_result(conn);
    MYSQL_ROW row = mysql_fetch_row(result);
    int id_user = atoi(row[0]); // Get the user ID

    // Step 4: Build the SQL query for games
    char query2[512];
    snprintf(query2, sizeof(query2), 
             "SELECT id_game, id_player_1, id_player_2, points_player_1, points_player_2, start_time, end_time "
             "FROM games WHERE (id_player_1 = %d OR id_player_2 = %d", id_user, id_user);
    
    // Step 5: Add start time condition if specified
    if (start_time[0] != '\0') {
        snprintf(query2 + strlen(query2), sizeof(query2) - strlen(query2), " AND start_time >= '%s'", start_time);
    }

    // Step 6: Add end time condition if specified
    if (end_time[0] != '\0') {
        snprintf(query2 + strlen(query2), sizeof(query2) - strlen(query2), " AND end_time <= '%s'", end_time);
    }
    
    strcat(query2, ")"); // Closing the WHERE clause

    // Step 7: Execute the second query
    mysql_query(conn, query2);
    result = mysql_store_result(conn);

    // Step 8: Prepare a response to send back to the client
    char response[2048] = "Games for the user:\n"; 
    while ((row = mysql_fetch_row(result))) {
        char game_info[256];
        snprintf(game_info, sizeof(game_info), 
                 "ID Game: %s, Player 1: %s, Player 2: %s, Points Player 1: %s, Points Player 2: %s, Start: %s, End: %s\n", 
                 row[0] ? row[0] : "NULL", 
                 row[1] ? row[1] : "NULL", 
                 row[2] ? row[2] : "NULL", 
                 row[3] ? row[3] : "NULL", 
                 row[4] ? row[4] : "NULL", 
                 row[5] ? row[5] : "NULL", 
                 row[6] ? row[6] : "NULL");
        
        strncat(response, game_info, sizeof(response) - strlen(response) - 1); // Append game info
    }

    mysql_free_result(result); // Free the second result set

    // Step 9: Send the response back to the client
    send(sock_conn, response, strlen(response), 0);

    // Close the client socket after communication is done
    close(sock_conn);
}

void show_rankings(int sock_conn)
{
    MYSQL *conn;          // Pointer to handle MySQL connection
    MYSQL_RES *res;      // Pointer to store the results of the query
    MYSQL_ROW row;       // Pointer to handle the returned rows

    // Initialize MySQL connection
    conn = mysql_init(NULL);
    mysql_real_connect(conn, "localhost", "root", "mysql", "battleship_database", 0, NULL, 0);
    
    // SQL query to obtain the user rankings by total points in ascending order
    const char *query = "SELECT username, total_points FROM users ORDER BY total_points ASC;";
    mysql_query(conn, query); // Execute the SQL query
    res = mysql_store_result(conn); // Store the results

    // Prepare to send results to the client
    char buffer[1024]; // Buffer for sending data
    int total_rows = mysql_num_rows(res); // Get the number of rows

    // Check if there are results
    if (total_rows > 0) {

        // Loop through the result set and send each row
        while ((row = mysql_fetch_row(res))) {
            // Format the username and total points in "username/total_points" format
            snprintf(buffer, sizeof(buffer), "%s/%s\n", row[0], row[1]);
            send(sock_conn, buffer, strlen(buffer), 0); // Send each row
        }
    } else {
        snprintf(buffer, sizeof(buffer), "No rankings available.\n");
        send(sock_conn, buffer, strlen(buffer), 0); // Send no results message
    }

    // Free the result and close the connection
    mysql_free_result(res);
    mysql_close(conn);
    close(sock_conn); // Close the socket connection
}
