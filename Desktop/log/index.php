<?php 
session_start();

	include("connection.php");
	include("functions.php");

    $dbhost = "localhost";
    $dbuser = "root";
    $dbpass = "";
    $dbname = "php";

    if(!$con = mysqli_connect($dbhost,$dbuser,$dbpass,$dbname))

	$user_data = check_login($con);

?>

<!DOCTYPE html>
<html>
<head>
	<title>My website</title>
</head>
<body>

	<a href="logout.php">Logout</a>
	<h1>This is the index page</h1>

	<br>
	Hello, <?php echo $user_data['user_name']; ?>
</body>
</html>