<?php 

    $serverName = "localhost";
    $dbUsername = "root";
    $dbPassword = "";
    $dbname = "account";

    //create connection
    mysqli_report(MYSQLI_REPORT_ERROR | MYSQLI_REPORT_STRICT);
    $conn = new MySQLI($serverName, $dbUsername, $dbPassword, $dbname);
    $conn->set_charset('utf8mb4'); // always set the charset

    if(isset($_POST['username'])){
        $username = $_POST['username'];
    }
    if(isset($_POST['password']))
    {
        $password = $_POST['password'];
        $ciphering = "AES-128-CTR";
        $iv_length = openssl_cipher_iv_length($ciphering);
        $options = 0;
        $encryption_iv = '1234567891011121';
        $encryption_key = "SchouIsABigChamp";
        $password = openssl_encrypt($password, $ciphering,
                $encryption_key, $options, $encryption_iv);
    
    
        $decryption_iv = '1234567891011121';
    
    // Store the decryption key
        $decryption_key = "SchouIsABigChamp";
    
    // Use openssl_decrypt() function to decrypt the data
        $decryption=openssl_decrypt ($password, $ciphering,
            $decryption_key, $options, $decryption_iv);

    }

    $con = new mysqli($serverName, $dbUsername, $dbPassword, $dbname) or die("Connection failed". mysqli_error());

    echo ("Connected!");
    echo ("<br>");
    
    $result = mysqli_query($con, "select * from users where username = '$username' and password = '$password'");
    
    
    if (!$result) {
        printf("Error: %s\n", mysqli_error($con));
        exit();
    }
    
    $row = mysqli_fetch_array($result);
    
    if($row['username'] == $username && $row['password'] == $password){
        session_start();
        echo("Weclome! ". $row['username']);
    }
    