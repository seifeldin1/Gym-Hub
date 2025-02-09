# **Gym-Hub** 🏋️‍♂️  

## **Overview**  
Gym-Hub is a comprehensive gym management system designed to streamline operations for gym owners, branch managers, coaches, and clients. This platform enables efficient management of workout plans, dietary schedules, financial reports, user roles, and gym-wide goals. It provides a structured approach to handling various aspects of gym administration, allowing seamless communication and coordination among all stakeholders.  

## **Key Features**  

### 🔹 **For Gym Owners**  
- **Gym-Wide Goal Setting:** Define and track objectives across all branches, visible in dashboards.  
- **Financial Overview:** Monitor expenses, revenue, and financial projections with interactive graphs.  
- **Comprehensive Reports:** Access detailed reports on finances, tasks, and performance metrics.  
- **User & Branch Management:** Move employees across branches, create/delete branches, and manage user assignments.  
- **Penalty & Bonus System:** Define, assign, and track penalties or bonuses for staff.  
- **Partner Management:** Manage profit shares and transactions for business partners.  
- **Branch Operating Hours:** Set and notify staff of gym timings.  
- **Salary Management:** View, update, and adjust staff salaries with historical records.  
- **Hiring Guidelines:** Define hiring criteria for branch managers and oversee recruitment processes.  

### 🔹 **For Branch Managers**  
- **Branch Reporting:** Submit monthly performance reports to the owner.  
- **Coaches Management:** Assign bonuses, penalties, and handle hiring/firing based on owner-defined criteria.  
- **Talent Pool Access:** Maintain a database of skilled candidates for future hiring.  
- **Contract Management:** Renew and extend coaches' contracts with tracking features.  
- **Meeting & Interview Scheduler:** Schedule and manage hiring and team meetings.  
- **Job Listings:** Post open positions with detailed role descriptions.  
- **Notice Board:** Publish announcements for gym staff and coaches.  

### 🔹 **For Coaches**  
- **Class & Client Management:** Track assigned clients, workout routines, and progress reports.  
- **Workout Assignments:** Assign workouts, set orders, and track client completion status.  
- **Client Meetings:** Schedule 1-on-1 sessions with clients.  
- **Self-Management:** View salary, bonuses, and penalty records.  
- **Announcements:** Post updates visible to clients regarding sessions and programs.  
- **Status Updates:** Set working status (e.g., "In a Meeting," "Available").  

### 🔹 **For Clients**  
- **Workout & Nutrition Management:** View assigned workout routines, progress updates, and dietary plans.  
- **Self-Management:** Choose goals, supplements, and plans suggested by coaches.  
- **Progress Tracking:** Track completed workouts and achievements.  
- **Notifications & Announcements:** Stay informed about gym events, changes, and personal schedules.  
- **Profile & Training Access:** Edit personal details, register for training sessions (HIIT, Cardio, Boxing, etc.).  

## **Technology Stack**  
- **Frontend:** React.js, Next.js, Tailwind CSS, Material UI  
- **Backend:** C#, .NET , Entity Framework  
- **Database:** MySQL  
- **Authentication:** JWT  

## Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/seifeldin1/Gym-Hub.git
   ```
2. Navigate to the project directory:
   ```sh
   cd Gym-Hub
   ```
3. Install dependencies (if applicable):
   ```sh
   dotnet restore
   ```
4. Configure the database (update connection strings and run migrations if needed).

## Usage
1. Run the application:
   ```sh
   dotnet run
   ```
2. Access the API/UI at `http://localhost:5291` (or specified port).

## Features
- User authentication & authorization (JWT-based)
- CRUD operations for entities
- Role-based access control (Owner, Branch Manager, Coach, Client)
- Branch management system for gym owners
- Workout and diet assignment by coaches
- Clients can view sessions, meetings, and assigned plans
- Secure database management using Entity Framework Core

## Contributing
1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m 'Add new feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a Pull Request.

## Contact
For any inquiries, contact [seif1442004@gmail.com].
