
#  Alon Sudoku Solver

 AlonSudoku is an advanced Sudoku solver written in **C#**, capable of handling **various board sizes** (4×4, 9×9, 16×16, 25×25). The solver utilizes multiple heuristics such as **Naked Singles, Hidden Singles** to efficiently solve puzzles.

---


## Features

- Supports **multiple Sudoku board sizes** (4×4, 9×9, 16×16, 25×25).  
-  Uses **heuristics-based solving techniques** for efficient solutions.  
-  **Two input methods**:  
   - **Console input** (manual entry of Sudoku).  
   - **File input** (loading Sudoku from a file).  
-  **Exception handling** for invalid inputs.  
-  **Unit tests** to validate Sudoku logic and handling of edge cases.  

---

##  Project Structure
![Image](https://github.com/user-attachments/assets/993eb697-11ae-4b11-85f9-acad86dc65c5)
![Image](https://github.com/user-attachments/assets/b0a60716-75e2-42f9-9d53-d88e89f0a1ce)


---

##  How the Solver Works

 **Board Initialization** – The board is stored as a array and initialized from user input.  
 **Preprocessing** – Constraints (row, column, and box) are calculated to eliminate impossible values.  
 **Applying Solving Techniques**:
   -  **Naked Singles** – If a cell has only one possibility, it is filled immediately.  
   -  **Hidden Singles** – If a number can only appear in one place in a row/column/box, it is placed there.  
 
 **Backtracking** – If heuristics alone don’t solve the board, the solver uses a **recursive search** to fill in missing values.  
 **Final Check** – The solution is validated against Sudoku rules.  

---

##  Running the Program

###  Using Visual Studio
1. Open AlonSudoku.sln in **Visual Studio**.  
2. Set AlonSudoku as the **Startup Project**.  
3. Click **Run (F5)**.  

###  Using Command Line
1. Navigate to the project directory: cd AlonSudoku
2. Build the project: dotnet Build
3. Run the program:dotnet run


---

## Unit Testing

To ensure the solver works correctly, a test project (AlonSudoku.Tests) is included.

###  Running Tests
1. Open a terminal in the project folder.  
2. Run:dotnet test

3. This will execute all unit tests and report if any fail.

###  What is Tested?
**Input validation tests** – Ensures incorrect inputs are rejected properly.  
**Solver logic tests** – Ensures Sudoku is solved correctly.    



---


###  Creator
**Alon Shemesh**




 





