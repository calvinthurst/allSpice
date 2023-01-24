CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY COMMENT 'primary KEY',
        title VARCHAR(50) NOT NULL DEFAULT 'moms spaghetti',
        instructions TEXT NOT NULL,
        img VARCHAR(255) NOT NULL DEFAULT 'https://images.unsplash.com/photo-1614961909013-1e2212a2ca87?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NXx8Y2Fzc2Vyb2xlfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=600&q=60',
        category VARCHAR(50) NOT NULL DEFAULT 'misc',
        creatorId VARCHAR(255) NOT NULL,
        Foreign Key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY COMMENT 'primary key',
        name VARCHAR(50) NOT NULL,
        quantity VARCHAR(50) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS favorite(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        accountId VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        Foreign Key (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        Foreign Key (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

select LAST_INSERT_ID() from favorite;