(ns game.ramp
 (require [arcadia.core :as a]
          [hard.core :as hard]
          [game.ui :as ui])
 (import [UnityEngine Resources]
         [Hover.InterfaceModules.Cast HovercastInterface])) 

(defn setup []
 (ui/handle-menu-cursors))

'(setup)