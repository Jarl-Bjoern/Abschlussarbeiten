#!/bin/sh
echo “------------------------------------------------------------------------------------“
echo “ SOFTWAREAKTUALISIERUNG “
echo “------------------------------------------------------------------------------------“
sudo apt-get -y update
echo “~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~“
echo “ Die Aktualisierung der Software wurde abgeschlossen. “
echo “~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~“
sudo apt-get -y dist-upgrade
echo “~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~“
echo “Die Aktualisierung des Betriebssystems wurde abgeschlossen. “
echo “~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~“
sudo apt -y autoremove
echo “~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~“
echo “ Veraltete Programme wurden erfolgreich entfernt. “
echo “~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~“
Datum=`date +‘%A %d.%m.%Y %H:%M:%S‘`
echo “Letzter Aktualisierungsstand: $Datum\n“ > /var/log/software_update.log