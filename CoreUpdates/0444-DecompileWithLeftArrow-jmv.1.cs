'From Cuis 2.0 of 24 February 2010 [latest update: #440] on 1 March 2010 at 10:51:36 am'!!AssignmentNode methodsFor: 'printing' stamp: 'jmv 2/28/2010 22:59'!printOn: aStream indent: level 	variable printOn: aStream indent: level.	aStream nextPutAll: ' _ '.	value printOn: aStream indent: level + 2! !!AssignmentNode methodsFor: 'printing' stamp: 'jmv 2/28/2010 22:59'!printWithClosureAnalysisOn: aStream indent: level 	variable printWithClosureAnalysisOn: aStream indent: level.	aStream nextPutAll: ' _ '.	value printWithClosureAnalysisOn: aStream indent: level + 2! !