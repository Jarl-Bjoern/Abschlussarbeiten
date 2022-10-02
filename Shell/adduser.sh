#!/bin/sh
echo “Es wurden folgende Parameter übergeben:“
echo “Name: $1“
echo “Gruppe: $2“
echo “Passwort: $3“
echo ““
CHECK=`sudo cat /etc/group | grep $2`

if [ $1 ] ; then
	if [ $2 ] ; then
		if [ $3 ] ; then
			if [ $CHECK ] ; then
				sudo useradd $1 -p $4 -g $3
				(echo $3; echo $3) | sudo smbpasswd -a $1
				echo ““
				echo “Der Benutzer $1 wurde erfolgreich angelegt!“
			else
				echo “Die Gruppe $2 existiert nicht!“
			fi
		else
			echo “Es wurde kein Passwort angegeben!“
		fi
	else
		echo “Es wurde keine Gruppe angegeben!“
	fi
else
	echo “Es wurden keine Parameter angegeben!“
fi