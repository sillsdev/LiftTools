copy /Y ..\palaso\output\debug\LiftIO.dll lib
REM copy /Y ..\palaso\output\debug\LiftIO.xml lib
REM copy /Y ..\palaso\output\debug\LiftIO.pdb lib

copy /Y ..\palaso\output\debug\Palaso.dll lib
copy /Y ..\palaso\output\debug\Palaso.xml lib
copy /Y ..\palaso\output\debug\Palaso.pdb lib
copy /Y ..\palaso\output\debug\Palaso.Lift.dll lib
copy /Y ..\palaso\output\debug\Palaso.Lift.xml lib
copy /Y ..\palaso\output\debug\Palaso.Lift.pdb lib

copy /Y ..\palaso\output\debug\Palaso.TestUtilities.* lib

REM copy /Y ..\palaso\output\debug\Palaso.BuildTasks.dll build

copy /Y lib\*.dll output\debug

pause