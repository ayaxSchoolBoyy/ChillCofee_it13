CREATE TABLE users
(
	id INT PRIMARY KEY IDENTITY(1,10),
	username VARCHAR(MAX) NULL,
	password VARCHAR(MAX) NULL,
	profile_image VARCHAR(MAX) NULL,
	role VARCHAR(MAX) NULL,
	status VARCHAR(MAX) NULL,
	date_rag DATE NULL
)

SELECT * FROM users

