#include <AutoItConstants.au3>

Global $WindowTitle="MOCSArcade"
If Not WinActive($WindowTitle) Then
MouseClick($MOUSE_CLICK_LEFT, 0, 175)
MouseMove(0, @DesktopHeight)
EndIf