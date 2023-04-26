<?php
// Create connection
$conn = new mysqli("localhost", "root", "K@mik@si1", "holamundo");

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
?>