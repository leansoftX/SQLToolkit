# SQL Toolkit for backup and execute sql scripts


## Backup Demo

```bash
SQLToolkit Backup -s $(DATABASE_SERVER) -n $(DATABASE_NAME) -u $(DATABASE_USERNAME) -p $(DATABASE_PASSWORD) -path /home/sqlbackup/database.bak
```

##  Execute Sqlscripts

```bash
SQLToolkit RunScripts -s $(DATABASE_SERVER) -n $(DATABASE_NAME) -u $(DATABASE_USERNAME) -p $(DATABASE_PASSWORD) -path ~/LabsUpgrade/SQLScripts_Up
```
