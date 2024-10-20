# SecureMessagingBackend

## Overview

**SecureMessagingBackend** is a server-side application built using ASP.NET Core, designed for secure user authentication and message management. This application implements key exchange protocols and encryption methods to facilitate secure communication between clients.

## Features

- **User Registration and Login**: Secure authentication with password validation.
- **Key Exchange**: Utilizes Diffie-Hellman key exchange for secure key sharing.
- **Message Encryption**: Symmetric encryption for secure message content.
- **Inbox Management**: Allows users to send and receive encrypted messages.

## Technologies Used

- **ASP.NET Core**: Web framework for building modern web applications.
- **Entity Framework Core**: ORM for data access.
- **Cryptography**: Implements AES for symmetric encryption and ECDH for key exchange.

## API Specification

### Base URL
`http://localhost:7244`

### Authentication

#### Register User
- **Endpoint**: `/auth/register`
- **Method**: `POST`
- **Request Body**:
  ```json
  {
    "email": "string",
    "password": "string",
    "name": "string"
  }
  ```
- **Responses**:
  - `200 OK`: User successfully registered.
  - `400 Bad Request`: Missing required fields.
  - `409 Conflict`: Email already exists.

#### Login User
- **Endpoint**: `/auth/login`
- **Method**: `POST`
- **Request Body**:
  ```json
  {
    "email": "string",
    "password": "string"
  }
  ```
- **Responses**:
  - `200 OK`: Login successful, returns user info.
  - `400 Bad Request`: Missing required fields.
  - `404 Not Found`: User not found.
  - `401 Unauthorized`: Incorrect password.

### Key Exchange

#### Exchange Keys
- **Endpoint**: `/key/exchangeKeys`
- **Method**: `POST`
- **Request Body**:
  ```json
  {
    "clientId": "string",
    "publicKey": "string"
  }
  ```
- **Responses**:
  - `200 OK`: Server public key returned.
  - `400 Bad Request`: Invalid public key.

### Messaging

#### Send Message
- **Endpoint**: `/message/send`
- **Method**: `POST`
- **Request Body**:
  ```json
  {
    "senderId": "int",
    "title": "string",
    "content": "string"
  }
  ```
- **Responses**:
  - `200 OK`: Message successfully sent.
  - `404 Not Found`: User not found.
  - `400 Bad Request`: Message not properly encrypted.

#### Get Inbox
- **Endpoint**: `/message/getInbox/{clientId}`
- **Method**: `GET`
- **Path Parameters**:
  - `clientId`: ID of the client whose inbox to retrieve.
- **Responses**:
  - `200 OK`: Returns inbox for the client.

