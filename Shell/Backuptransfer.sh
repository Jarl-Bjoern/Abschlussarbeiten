#!/bin/sh
echo “------------------------------------------------------------------------------------“
echo “ BACKUPTRANSFER “
echo “------------------------------------------------------------------------------------“
sudo cp -rf /media/cloud /media/backup
Datum=`date +’%A %d.%m.%Y %H:%M:%S’`
echo “Letzter Kopiervorgang: $Datum\n“ >> /var/log/backuptransfer.log