const create = document.querySelector('#create');
const login = document.querySelector('#login');

var userNameReg = document.querySelector('#userNameReg');
var passwordReg = document.querySelector('#passwordReg');
var emailReg = document.querySelector('#emailReg');
var firstnameReg = document.querySelector('#firstNameReg');
var lastNameReg = document.querySelector('#lastNameReg');
var specialtyReg = document.querySelector('#Specializing');

var userNameLogin = document.querySelector('#userNameLogin');
var passwordLogin = document.querySelector('#passwordLogin');

//for posts req:
function LoginPostReq(){
    let jsObjLogin = new Object();
    jsObjLogin = {UserName: userNameLogin.value, Password: passwordLogin.value};
    console.log(jsObjLogin);
    APIMethod('Login', 'POST', JSON.stringify(jsObjLogin));
}

function RegisterPostReq(){
    let jsObj = new Object();
var passwordReg = document.querySelector('#passwordReg');
    jsObj = {UserName: userNameReg.value, Password: passwordReg.value, Email: emailReg.value, FirstName: firstnameReg.value, LastName: lastNameReg.value, Specialty: specialtyReg.value};
    console.log(jsObj);
    APIMethod('User', 'POST', JSON.stringify(jsObj));
}

function LoginGetReq(){
	APIMethod('Login', 'GET');
}

function APIMethod(api, method, content){
  var xhttp = new XMLHttpRequest();
  xhttp.open(method, 'https://localhost:44390/api/' + api, true);
  xhttp.setRequestHeader('Content-Type', 'application/json');
  xhttp.onload = function(){
 	console.log(xhttp.responseText);  //change here
 }
 xhttp.send(content);
}

create.addEventListener("click", () => {
    RegisterPostReq();
});

login.addEventListener("click", () => {
    LoginPostReq();
});