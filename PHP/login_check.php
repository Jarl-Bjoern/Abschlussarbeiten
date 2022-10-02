<?php
	if (isset($_POST['submit'])) {
		# Übertragung der Werte aus den Textfeldern
		$user = strtolower($_POST['user']);
		$pwd = md5($_POST['pwd']);

		# Erzeugung von Session Variablen
		if (!isset($_SESSION['logincount']) & !isset($_SESSION['active'])) {
			$_SESSION['logincount'] = 0;
			$_SESSION['active'] = 1;
		}
		
		if (empty($_POST['user']) || empty($_POST['pwd'])) {
			$error = 1;
		} else {
			# Aufbau der Verbindung zur Datenbank
			require_once ‘pw_check.php‘;
			$con = new mysqli('localhost', 'admin', $pw, 'control');

			if ($con == false) {
				$error = 2;
			} else {
			try {
				# Überprüfung der Datenbank nach Benutzersperrung
				$brief_user = $con->prepare("SELECT id, activated FROM users WHERE name=?
				AND activated=?");
				$brief_user->bind_param("si", $user, $_SESSION['active']);
				$brief_user->execute();
				$brief_result = $brief_user->get_result();
				$brief_user->close();
				
				# Ausgabe der SQL-Werte
				if ($brief_result->num_rows != 0) {
					# Überprüfung des Benutzerdaten
					$search_user = $con->prepare("SELECT id, activated FROM users WHERE name=? AND password=?");
					$search_user->bind_param("ss", $user, $pwd);
					$search_user->execute();
					$search_result = $search_user->get_result();
					$search_user->close();

					if ($_SESSION['logincount'] <= 3) {
						if ($search_result->num_rows != 0) {
							$search_object = $search_result->fetch_object();
							$_SESSION['benutzer'] = $search_object->id;
							header('Refresh:0');
						} else {
							$_SESSION['logincount']++;
							if ($_SESSION['logincount'] > 0 & $_SESSION['logincount'] <= 2) {
								$error = 3;
								shell_exec ('echo "WARNUNG!\nFehlerhafte Anmeldung!\n".$user.' >> /var/log/login_fehler.log);
								shell_exec('date +%A“ “%d“.“%m“.“%Y“ “%H“:“%M“:“%S >> /var/log/login_fehler.log');
								shell_exec ('echo "~~~~~~~~~~~~~~~"' >> /var/log/login_fehler.log);
								header('Refresh:3');
							} else if ($_SESSION['logincount'] == 3) {
								# Sperrung des Benutzers
								$_SESSION['active'] = 0;
								$deactivate_user = $con->prepare("UPDATE users SET activated=? WHERE name=?");
								$deactivate_user->bind_param("is", $_SESSION['active'], $user);
								$deactivate_user->execute();
								$deactivate_user->close();
								shell_exec ('echo true > /usr/bin/blocked.txt');
								shell_exec ('echo Der Administrator '.$user.' wurde gesperrt! >> /usr/bin/user.txt');
								$error = 4;
							}
						}
					}
				} else {
					$check_user = $con->prepare("SELECT id FROM users WHERE name=?");
					$check_user->bind_param("s", $user);
					$check_user->execute();
					$check_result = $check_user->get_result();
					
					if ($check_result->num_rows == 0) {
						$error = 5;
					} else {
						$error = 4;
					}
					header('Refresh:3');
				}
			}
		catch (Exception $e) {
			throw new \Exception('Error: ' . $e->getMessage());
		}
		$con->close();
	}
} ?>
<table frame="box" rules="all">
	<th><font>LEER</font></th>
	<tr>
		<td><?php
			if ($error == 1) {
				echo ('<span style="color:white;font-size:23;">Die Anmeldedaten sind unvollständig!');
			} else if ($error == 2) {
				echo ('<span style="color:white;font-size:23;">Es konnte keine Verbindung zur Datenbank aufgebaut werden!');
			} else if ($error == 3) {
				echo ('<span style="color:white;font-size:23;">Die eingegebenen Benutzerdaten sind fehlerhaft!');
			} else if ($error == 4) {
				echo ('<span style="color:white;font-size:23;">Der Benutzer wurde aus Sicherheitsgründen gesperrt!');
				session_destroy();
			} else if ($error == 5) {
				echo ('<span style="color:white;font-size:23;">Der Benutzer existiert nicht!');
			} ?>
		</td>
	</tr>
</table>