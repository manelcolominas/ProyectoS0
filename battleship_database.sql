-- Drop existing database (if needed) and create a new one
DROP DATABASE IF EXISTS M8_battleship_database;
CREATE DATABASE M8_battleship_database;
USE M8_battleship_database;

-- Create the 'users' table
CREATE TABLE users (
    ID INT PRIMARY KEY AUTO_INCREMENT, -- Unique identifier for each user
    username VARCHAR(100) NOT NULL UNIQUE,  -- Unique username
    password VARCHAR(100) NOT NULL,         -- Store hashed passwords here
    email VARCHAR(100) NOT NULL UNIQUE,     -- Unique email
    total_points INT DEFAULT 0              -- Tracks total points earned by user
);

-- Create the 'games' table
CREATE TABLE games (
    id_game INT PRIMARY KEY AUTO_INCREMENT,   -- Unique identifier for each game
    id_player_1 INT NOT NULL,                 -- Reference to player 1
    id_player_2 INT NOT NULL,                 -- Reference to player 2
    points_player_1 INT DEFAULT 0,            -- Points scored by player 1
    points_player_2 INT DEFAULT 0,            -- Points scored by player 2
    start_time DATE,                      -- Start time of the game
    end_time DATE,                        -- End time of the game
    FOREIGN KEY (id_player_1) REFERENCES users(ID) ON DELETE CASCADE, -- Ensure user reference exists
    FOREIGN KEY (id_player_2) REFERENCES users(ID) ON DELETE CASCADE, -- Ensures both player references
    CHECK (id_player_1 != id_player_2)        -- Ensure that player 1 and 2 are different
);

-- Insert into users table
INSERT INTO users (username, password, email, total_points)
VALUES 
    ('manel007', '12345', 'manel.colominas@estudiantat.upc.edu', 150),
    ('Hugo123', '12345', 'hugo@example.com', 180),
    ('Laura456', '12345', 'laura@example.com', 130),
    ('Esther789', '12345', 'esther@example.com', 90),
    ('User1', '12345', 'user1@example.com', 160),
    ('User2', '12345', 'user2@example.com', 120);

-- Insertar partidas en la tabla games
INSERT INTO games (id_player_1, id_player_2, points_player_1, points_player_2, start_time, end_time)
VALUES
	 -- Partida 1: manel007 vs Hugo123
	(1, 2, 100, 80, '2024-10-10', '2024-10-10'),  -- manel007 gana 100, Hugo 80
	-- Partida 2: Laura456 vs Esther789
	(3, 4, 60, 90, '2024-10-11', '2024-10-11'),   -- Esther gana 90, Laura 60
	-- Partida 3: Hugo123 vs User1
	(2, 5, 50, 90, '2024-10-12', '2024-10-12'),   -- User1 gana 90, Hugo 50
	-- Partida 4: User2 vs manel007
	(6, 1, 60, 50, '2024-10-13', '2024-10-13'),   -- User2 gana 60, manel007 50
	-- Partida 5: Laura456 vs User1
	(3, 5, 70, 70, '2024-10-14', '2024-10-14'),   -- Empate, ambos obtienen 70
	-- Partida 6: Esther789 vs Hugo123
	(4, 2, 0, 50, '2024-10-15', '2024-10-15'),    -- Hugo gana 50, Esther 0
	-- Partida 7: User2 vs Laura456
	(6, 3, 60, 50, '2024-10-16', '2024-10-16'),   -- User2 gana 60, Laura 50
	-- Partida 8: manel007 vs Esther789
	(1, 4, 0, 60, '2024-10-17', '2024-10-17'),    -- Esther gana 60, manel007 0
	-- Partida 9: User1 vs User2
	(5, 6, 80, 0, '2024-10-18', '2024-10-18'),    -- User1 gana 80, User2 0
	-- Partida 10: Hugo123 vs Laura456
	(2, 3, 0, 50, '2024-10-19', '2024-10-19');    -- Laura gana 50, Hugo 0
