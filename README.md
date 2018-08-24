SQL Toolkit for backup and execute sql scripts

1. Backup Demo

SQLToolkit Backup -s $(DATABASE_SERVER) -n $(DATABASE_NAME) -u $(DATABASE_USERNAME) -p $(DATABASE_PASSWORD) -path /home/sqlbackup/database.bak

2. Execute Sqlscripts

SQLToolkit RunScripts -s $(DATABASE_SERVER) -n $(DATABASE_NAME) -u $(DATABASE_USERNAME) -p $(DATABASE_PASSWORD) -path ~/LabsUpgrade/SQLScripts_Up
