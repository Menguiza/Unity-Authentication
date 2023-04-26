<?php
require "ConnectionSettings.php";

$registerUser = $_POST["registerUser"];
$registerEmail = $_POST["registerEmail"];
$registerPass = $_POST["registerPass"];

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$sql = "SELECT username FROM user where username = ?";

$statement = $conn->prepare($sql);

$statement->bind_param("s", $registerUser);

$statement->execute();

$result = $statement->get_result();

if ($result->num_rows > 0) {
  echo "401";
} else {
    $sql = "SELECT email FROM user where email = ?";

    $statement = $conn->prepare($sql);

    $statement->bind_param("s", $registerEmail);

    $statement->execute();

    $result = $statement->get_result();

    if ($result->num_rows > 0)
    {
        echo "402";
    } else{
        $sql = "INSERT INTO user (email, username, password, score)
        VALUES ('".$registerEmail."', '".$registerUser."', '".$registerPass."', 0)";

        if ($conn->query($sql) === TRUE) {
            echo "200";
        } else {
            echo "Error: " . $sql . "<br>" . $conn->error;
        }
    }
}

$conn->close();

?>