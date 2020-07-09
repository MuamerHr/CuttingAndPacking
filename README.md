# CuttingAndPacking

A brief description of the one dimensional cutting stock problem
and one way to solve the problem exactly can be read in file "Description.pdf".

The repository contains a console program and a library with two common 
heuristics for the one dimensional cutting stock problem and a procedure 
to create the matrix of cutting patterns and to store it into a
"AMPL-DATA" file. 

This data file together with the 
- "AMPL-MODEL" file "cuttingStockMod.mod" and the 
- "AMPL-COMMANDS" file "commands.run" 
located in the folder "CuttingAndPacking/CuttingStock/AMPL/" can be 
used as input for a solver which implements the AMPL interface. 

A list of solvers which implement the AMPL interface and which 
can be used for testing this model and others can be found on
https://neos-server.org/neos/solvers/index.html.
