<?php
include_once 'ConnectionSettings.php';

$result = mysqli_query($conn, "SELECT username, score FROM user");

if ($result->num_rows > 0) {
  $row = array();
  // output data of each row
  while($row = $result->fetch_assoc()) {
    $rows[] = $row;
  }

  echo json_encode($rows);

} else {
  echo "0";
}

$conn->close();

?>