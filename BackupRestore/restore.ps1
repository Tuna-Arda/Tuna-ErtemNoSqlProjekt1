# Restore-Skript für MongoDB mit mongorestore
# Stelle sicher, dass mongorestore im PATH verfügbar ist oder gib den vollständigen Pfad an

$mongoHost = "localhost"
$mongoPort = "27017"
$database = "JetstreamDB"
$backupDir = Read-Host "Gib den Pfad zum Backup-Verzeichnis ein"

mongorestore --host $mongoHost --port $mongoPort --db $database --username MyMigrationUser --password "<password>" $backupDir

Write-Host "Restore abgeschlossen von $backupDir"
