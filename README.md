# Abituriyent

Abituriyent is a web application designed to provide information and resources for students. The application includes features such as contact details, happy student testimonials, and a contact form.

I have built this platform in 2015 for commercial purposes and then made it open sourced to show how the production ready application can be built using Asp.Net Core 1.0

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

## Features

- Ability to manage all stuff from admin panel
- Course, Lesson, School, Teacher, Student, and User management and much more
- Online exams and complete learning platform
- Display contact details including website, email, phone, and location.
- Showcase testimonials from happy students.
- Provide a contact form for users to send messages.

## Technologies

- **Languages**: JavaScript, C#
- **Frameworks**: ASP.NET Core, Entity Framework Core 1.0

## Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/nurlanvalizada/Abituriyent.git
    ```
2. Navigate to the project directory:
    ```sh
    cd Abituriyent
    ```
3. Apply migrations:
    ```sh
    dotnet ef database update
    ```
4. Build the project:
    ```sh
    dotnet build
    ```

## Usage

1. Run the application:
    ```sh
    dotnet run
    ```
2. Open your browser and navigate to `http://localhost:5000`.
