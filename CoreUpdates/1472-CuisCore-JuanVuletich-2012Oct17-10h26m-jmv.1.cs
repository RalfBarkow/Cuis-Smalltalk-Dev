'From Cuis 4.0 of 21 April 2012 [latest update: #1471] on 17 October 2012 at 11:00:08 am'!

!Integer methodsFor: 'benchmarks' stamp: 'jmv 10/17/2012 10:30'!
tinyBenchmarks
	"Report the results of running the two tiny Squeak benchmarks.
	ar 9/10/1999: Adjusted to run at least 1 sec to get more stable results"
	"0 tinyBenchmarks"
	"On a 292 MHz G3 Mac: 22,727,272 bytecodes/sec; 984,169 sends/sec"
	"On a 400 MHz PII/Win98:  18,028,169 bytecodes/sec; 1,081,272 sends/sec"
	"On a 1.6GHz Atom/Win7 with Cog:  201,099,764 bytecodes/sec; 28,197,075 sends/sec"
	| t1 t2 r n1 n2 |
	n1 _ 1.
	[
		t1 _ Time millisecondsToRun: [n1 benchmark].
		t1 < 1000] 
			whileTrue:[n1 _ n1 * 2]. "Note: #benchmark's runtime is about O(n)"

	n2 _ 28.
	[
		t2 _ Time millisecondsToRun: [r _ n2 benchFib].
		t2 < 1000] 
			whileTrue:[n2 _ n2 + 1]. 
	"Note: #benchFib's runtime is about O(k^n),
		where k is the golden number = (1 + 5 sqrt) / 2 = 1.618...."

	^ ((n1 * 500000 * 1000) // t1) asStringWithCommas, ' bytecodes/sec; ',
	  ((r * 1000) // t2) asStringWithCommas, ' sends/sec'! !

