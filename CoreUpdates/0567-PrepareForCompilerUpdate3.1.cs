'From Cuis 2.6 of 10 August 2010 [latest update: #540] on 28 August 2010 at 10:13:37 pm'!!classDefinition: #BytecodeAgnosticMethodNode category: #'Compiler-ParseNodes'!MethodNode subclass: #BytecodeAgnosticMethodNode	instanceVariableNames: 'locationCounter localsPool zzlocationCounter zzlocalsPool '	classVariableNames: ''	poolDictionaries: ''	category: 'Compiler-ParseNodes'!!BytecodeAgnosticMethodNode methodsFor: 'code generation (closures)' stamp: 'jmv 8/28/2010 22:13'!addLocalsToPool: locals "<Set of: TempVariableNode>"	zzlocalsPool ifNil: [		zzlocalsPool := IdentitySet new].	zzlocalsPool addAll: locals! !!classDefinition: #BytecodeAgnosticMethodNode category: #'Compiler-ParseNodes'!MethodNode subclass: #BytecodeAgnosticMethodNode	instanceVariableNames: 'zzlocationCounter zzlocalsPool'	classVariableNames: ''	poolDictionaries: ''	category: 'Compiler-ParseNodes'!