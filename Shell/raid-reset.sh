#!/bin/sh
if [ `sudo cat /proc/mdstat | grep resync | cut -d“ “ -f9`] ; then
{
	echo “Das RAID-System befindet sich bereits in einem Wiederaufbau!“
	echo “false“ > /usr/bin/xvR.txt
}
else
{
	if [ `sudo cat /usr/bin/xvR.txt | grep true` ] ; then
	{
		sudo umount /dev/md0
		sudo mdadm --stop /dev/md0
		F1=“/dev/“`sudo ls -al /dev/disk/by-id | grep “00800-0:0-part1“ | cut -d“/“ -f3`
		F2=“/dev/“`sudo ls -al /dev/disk/by-id | grep “00800-0:0-part2“ | cut -d“/“ -f3`
		F3=“/dev/“`sudo ls -al /dev/disk/by-id | grep “15221-0:0-part3“ | cut -d“/“ -f3`
		F4=“/dev/“`sudo ls -al /dev/disk/by-id | grep “15221-0:0-part4“ | cut -d“/“ -f3`
		HS1=“/dev/“`sudo ls -al /dev/disk/by-id | grep “15221-0:0-part1“ | cut -d“/“ -f3`
		HS2=“/dev/“`sudo ls -al /dev/disk/by-id | grep “15221-0:0-part2“ | cut -d“/“ -f3`
		sudo mdadm --zero-superblock $F1
		sudo mdadm --zero-superblock $F2
		sudo mdadm --zero-superblock $F3
		sudo mdadm --zero-superblock $F4
		sudo mdadm --zero-superblock $HS1
		sudo mdadm --zero-superblock $HS2
		echo “yes“ | sudo mdadm --create /dev/md0 --level=10 --raid-devices=4 $F1 $F2 $F3 $F4
		echo “yes“ | sudo mkfs.ext4 –b 4096 -E stride=128,stripe-width=256 /dev/md0
		sudo su -c “/usr/share/mdadm/mkconf > /etc/mdadm/mdadm.conf“
		sudo touch /etc/mdadm/mdadm_p.conf
		sudo touch /etc/mdadm/t.conf
		sudo chmod 666 /etc/mdadm/mdadm_p.conf
		sudo chmod 666 /etc/mdadm/t.conf
		sudo cat /etc/mdadm/mdadm.conf | grep HOME > /etc/mdadm/mdadm_p.conf
		sudo mdadm --detail /dev/md0 | grep Version | cut -d: -f2 > /etc/mdadm/t.conf
		META=`sudo cat /etc/mdadm/t.conf | cut -d“ “ -f2`
		sudo mdadm --detail /dev/md0 | grep UUID | cut -d: -f2-5 > /etc/mdadm/t.conf
		UUID=`sudo cat /etc/mdadm/t.conf | cut -d“ “ -f2`
		echo “ARRAY /dev/md/0 metadata=$META UUID=$UUID“ >> /etc/mdadm/mdadm_p.conf
		sudo mv /etc/mdadm/mdadm_p.conf /etc/mdadm/mdadm.conf
		sudo chmod 644 /etc/mdadm/mdadm.conf
		sudo rm /etc/mdadm/t.conf
		sudo update-initramfs -u
		sudo mount -a
		sudo mdadm /dev/md0 --add $HS1
		sudo mdadm /dev/md0 --add $HS2
		echo “false“ > /usr/bin/xvR.txt
		Datum=`date +‘%A %d.%m.%Y %H:%M:%S‘`
		echo „Letzter Neuaufbau: $Datum\n“ >> /var/log/rreset.log
	}
	fi
}
fi