<?php

$username = filter_input(INPUT_POST, 'username');
$password = filter_input(INPUT_POST, 'password');
$email = filter_input(INPUT_POST, 'email');

if ($username && $password && $email) {
    $serverName = "localhost";
    $dbUsername = "root";
    $dbPassword = "";
    $dbname = "account";

    //create connection
    mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
    $conn = new MySQLI($serverName, $dbUsername, $dbPassword, $dbname);
    $conn->set_charset('utf8mb4'); // always set the charset

    //Prepare statement
    $stmt = $conn->prepare("SELECT COUNT(email) From users Where email = ? Limit 1");
    $stmt->bind_param("s", $email);
    $stmt->execute();
    $stmt->store_result();
    $stmt->bind_result($exists); // we fetch the count
    $stmt->fetch();
    if (!$exists) {
        $stmt = $conn->prepare("INSERT Into users (username, password, email) values(?, ?, ?)");
        // Don't forget to hash the password and never store the real password anywhere
        //$hash = password_hash($password, PASSWORD_DEFAULT);

        $ciphering = "AES-128-CTR";
        $iv_length = openssl_cipher_iv_length($ciphering); 
        $options = 0; 
        $encryption_iv = '1234567891011121'; 
        $encryption_key = "SchouIsABigChamp"; 
        $encryption = openssl_encrypt($password, $ciphering, 
        $encryption_key, $options, $encryption_iv);

        $stmt->bind_param("sss", $username, $encryption, $email);
        $stmt->execute();
        echo "New record inserted sucessfully";
    } else {
        echo "Someone already register using this email";
    }
} else {
    echo "All field are required";
}