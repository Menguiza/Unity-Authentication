<?php
include_once 'ConnectionSettings.php';

$userId = $_POST["userId"];
$score = $_POST["score"];

$result = mysqli_query($conn, "UPDATE user SET score = ".$score." WHERE id = ".$userId);

if ($result) {
  echo "200";
  //var_dump($result);
} else {
  echo "0";
}

$conn->close();

?>