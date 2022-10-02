#!/bin/sh
if [ `sudo cat /proc/mdstat | grep resync | cut -d" " -f9` ] ; then
{
	echo “Das RAID-System befindet sich in einem Wiederaufbau!”
}
else
	if [ `sudo cat /usr/bin/xvD.txt | grep true` ] ; then
	{
		sudo cp -rf /media/backup/cloud/herold.rainer/files/daten/* /media/daten
		echo “false“ > /usr/bin/xvD.txt
		sudo bash /usr/bin/change.bash
		Datum=`date +‘%A %d.%m.%Y %H:%M:%S‘`
		echo “Letzte Datenwiederherstellung: $Datum\n“ >> /var/log/datenwiederherstellung.log
	}
	fi
fi