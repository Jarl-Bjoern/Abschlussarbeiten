#!/bin/bash
DIR="/media/daten"
CHECK=`sudo ls -al $DIR | grep -E 'IT|Mitarbeiter|Leitung' | cut -d" " -f4`
DATA=`sudo ls -R $DIR/*/* | grep ":$" | sed -e 's/:$//' && sudo find $DIR/*/* -iname "*.*"`
if [[ $CHECK =~ "root" ]] ; then
	sudo chown root.IT "$DIR/IT"
	sudo chown root.Leitung "$DIR/Leitung"
	sudo chown root.Mitarbeiter "$DIR/Mitarbeiter"
	sudo chmod 1770 "$DIR/IT" "$DIR/Mitarbeiter" "$DIR/Leitung"
	
	IFS=$'\n'
	for data in ${DATA[@]} ; do
		if [[ "${data}" =~ "IT" ]] ; then
			sudo chown "Abt-IT".IT "${data}"
		elif [[ "${data}" =~ "Mitarbeiter" ]] ; then
			sudo chown "Abt-Mitarbeiter".Mitarbeiter "${data}"
		elif [[ "${data}" =~ "Leitung" ]] ; then
			sudo chown "Abt-Leitung".Leitung "${data}"
		fi
		if [[ -d "${data}" ]] ; then
			sudo chmod 700 "${data}"
		elif [[ -f "${data}" ]] ; then
			sudo chmod 600 "${data}"
		fi
	done
fi