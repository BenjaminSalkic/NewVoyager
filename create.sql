-- Create a new database called 'NewVoyager'
-- Connect to the 'master' database to run this snippet
USE master
GO
-- Create the new database if it does not exist already
IF NOT EXISTS (
    SELECT name
        FROM sys.databases
        WHERE name = N'NewVoyager'
)
CREATE DATABASE NewVoyager
GO