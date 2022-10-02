<?php
session_start();

# Kopfzeile
require_once 'header.php';

# Bodybereich
if (isset($_SESSION['benutzer'])) {
	require('pages/'.'admincontrol.php');
} else {
	require_once('pages/'.'login.php');
}

# Login_Überprüfung
require_once('pages./’.’login_check.php');

# Fußzeile
require_once('footer.php');
?>