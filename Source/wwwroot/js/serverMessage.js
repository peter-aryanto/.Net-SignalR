"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ClientSideMessaging").build();
var isConnected = false;

//Disable the send button until connection is established.
//document.getElementById("sendButton").disabled = true;

//connection.on("ReceiveMessage", function (user, message) {
//connection.on("ProcessMessageFromServer", function (message) {
connection.on("ProcessBroadcastMessageFromServer", function (message) {
  /*
  var li = document.createElement("li");
  document.getElementById("messagesList").appendChild(li);
  // We can assign user-supplied strings to an element's textContent because it
  // is not interpreted as markup. If you're assigning in any other way, you 
  // should be aware of possible script injection concerns.
  li.textContent = `${user} says ${message}`;
  */
  document.getElementById("serverMessageDisplayContainer").innerText = message;
});

connection.start().then(function () {
  //document.getElementById("sendButton").disabled = false;
  isConnected = true;
  document.initServerMessage(connection);
}).catch(function (err) {
  return console.error(err.toString());
});

/*
document.getElementById("sendButton").addEventListener("click", function (event) {
  var user = document.getElementById("userInput").value;
  var message = document.getElementById("messageInput").value;
  connection.invoke("SendMessage", user, message).catch(function (err) {
    return console.error(err.toString());
  });
  event.preventDefault();
});
*/
