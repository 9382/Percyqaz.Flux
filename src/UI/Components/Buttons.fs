﻿namespace Percyqaz.Flux.UI

open Percyqaz.Flux.Input

type Button(text: string, onClick: unit -> unit, hotkey: Hotkey) as this =
    inherit StaticContainer(NodeType.Button onClick)
    
    let color = Animation.Fade(0.0f)
    let colorFunc = Palette.text_transition color Palette.LIGHT Palette.WHITE

    do
        this
        |+ Text(
            text,
            Align = Alignment.CENTER,
            Color = colorFunc)
        |+ Clickable.Focus this
        |* HotkeyAction(hotkey, onClick)

    override this.OnFocus() =
        base.OnFocus()
        color.Target <- 1.0f

    override this.OnUnfocus() =
        base.OnUnfocus()
        color.Target <- 0.0f

    override this.Update(elapsedTime, moved) =
        base.Update(elapsedTime, moved)
        color.Update(elapsedTime)
        if this.Selected then this.Focus()