<?php
require "ConnectionSettings.php";

$loginUser = $_POST["loginUser"];
$loginPass = $_POST["loginPass"];

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT password, id FROM user where username = ?";

$statement = $conn->prepare($sql);

$statement->bind_param("s", $loginUser);

$statement->execute();

$result = $statement->get_result();

if ($result->num_rows > 0) {
  // output data of each row
  while($row = $result->fetch_assoc()) {
    if($row["password"] == $loginPass) echo $row["id"].",200";
    else echo "400";
  }
} else {
  echo "404";
}

$conn->close();

?>