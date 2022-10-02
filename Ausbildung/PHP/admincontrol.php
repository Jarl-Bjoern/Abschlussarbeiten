<?php
	require_once ‘pw_check.php‘;
	$con = new mysqli('localhost','admin', $pw,'control');
	if ($con == false) {
		die();
	}
	
	$search_user = $con->prepare("SELECT * FROM users WHERE id=?");
	$search_user->bind_param('i',$_SESSION['benutzer']);
	$search_user->execute();
	$search_result = $search_user->get_result();
	$search_user->close();
	
	if ($search_result->num_rows == 1) {
		$search_object = $search_result->fetch_object();
		$name = $search_object->name;
	}
	$con->close();
	
	if (isset($_SESSION['benutzer'])) {
?>
<h1>Administrative Einstellungen</h1>
<hr>
<h2>Sie sind als Benutzer <?php echo $name; ?> angemeldet!</h2>
<hr>
<form method="POST">
	<table frame="box" rules="all">
	<th><h2>Datenwiederherstellung</h2></th> <th><font>LEER</font></th>
	<th><h2>RAID-Neuaufbau</h2><th>
		<tr>
			<td><input type="submit" name="databackup" value="Wiederherstellen" /></td>
			<td><font>LEER</font></td>
			<td><input type="submit" name="raidreset" value="Neuaufbau des RAIDs" /></td>
		</tr>
	</table>
	<table frame="box" rules="all">
	<th><h2>Weiterleitung zu Nextcloud</h2></th>
		<tr>
			<td><input type="submit" name="nextcloud" value="Bestätigen" /></td>
		</tr>
	</table>
	<hr>
</form>
<table frame="box" rules="all">
	<th></th>
		<tr>
			<td>
				<?php
				if (isset($_POST['databackup'])) {
					shell_exec ('echo true > /usr/bin/xvD.txt');
					echo ('<span style="color:white;font-size:23;">Die Datenwiederherstellung wurde eingeleitet!');
				}
				else if (isset($_POST['raidreset'])) {
					shell_exec ('echo true > /usr/bin/xvR.txt');
					echo ('<span style="color:white;font-size:23;">Der Neuaufbau des RAID-Verbundes wurde eingeleitet!');
				}
				else if (isset($_POST['nextcloud'])) {
					header(“location: http://cloud.hausarbeit.hbfwi“);
					session_destroy();
					exit();
				} ?>
			</td>
		</tr>
</table>
<?php } else {
	echo ('<span style="color:white;font-size:23;">Sie sind nicht angemeldet!');
} ?>
