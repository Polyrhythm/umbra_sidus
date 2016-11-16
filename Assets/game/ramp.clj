(ns game.ramp
 (require [arcadia.core :as a]
          [hard.core :as hard])
 (import [UnityEngine Resources]))

(defn setup []
 (hard/clear-cloned!)
 (let [camera (hard/clone! :cameras/lm-rig)
       i-manager (hard/clone! :managers/interaction-manager)]))
