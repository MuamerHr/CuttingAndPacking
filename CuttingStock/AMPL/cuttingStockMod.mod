set lengths;									#set of indices for the lengths
set col;										#set of indices for the columns of the matrix of cutting patterns

param cutScheme{col,lengths} >= 0;				#the matrix of cutting patterns
param amounts{lengths} >=0;						#number of cuttings of the lengths
			
var x {j in col} integer >= 0;	#variable to indicate how often cutting pattern cutScheme[*,j] is used

#Cuttingstock Problem
minimize target: sum {j in col} x[j];

subject to max_length {i in lengths}:
	sum {j in col} cutScheme[j,i] * x[j] >= amounts[i];