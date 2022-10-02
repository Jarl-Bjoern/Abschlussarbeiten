<?php
	$Datei = fopen(“pages/usr/pw.txt“, “r“) or die (“Die Datei lässt sich nicht öffnen!“);
	$Text = fread($Datei, filesize(“pages/usr/pw.txt“));
	$pw = preg_replace('/\s+/', '', $Text);
	fclose($Datei);
?>