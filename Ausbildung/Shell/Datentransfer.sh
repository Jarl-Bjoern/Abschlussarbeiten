#!/bin/sh
echo “------------------------------------------------------------------------------------“
echo “ DATENTRANSFER “
echo ”------------------------------------------------------------------------------------“
sudo cp -rf /media/daten/* /media/cloud/herold.rainer/files/daten/*
echo ”~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~“
echo ” Der Kopiervorgang wurde abgeschlossen. “
echo ”~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~“
sudo -u www-data php /var/www/html/nextcloud/console.php files:scan --all
Datum=`date +‘%A %d.%m.%Y %H:%M:%S‘`
echo “Letzter Kopiervorgang: $Datum\n“ >> /var/log/datentransfer.log