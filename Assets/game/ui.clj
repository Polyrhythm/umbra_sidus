(ns game.ui
 (require [arcadia.core :as a]
          [hard.core :refer :all])
 (import [Hover.InterfaceModules.Cast HovercastInterface]
         [Hover.Core.Cursors CursorCapabilityType HoverCursorData])) 

(defn add-hovercast-open-listener [cast-go callback]
 (let [cast (a/cmpt cast-go HovercastInterface)]
  (.. cast OnOpenToggledEvent
   (AddListener callback))))
   
(defn set-interactions [menu-open?]
 (let [cursors (the cursors)
       left-hand-parts (-> (child-named cursors "LeftHand")
                           (a/children))]
  (if menu-open?
   (doseq [part left-hand-parts]
    (cond
     (= "LeftPalm" (.name part)) (set! (._Capability part) CursorCapabilityType/TransformOnly)
     :else (set! (._Capability part) CursorCapabilityType/None)))
   (doseq [part left-hand-parts]
    (set! (._Capability part) CursorCapabilityType/Full))))) 

(defn handle-menu-cursors []
 (let [cast-go (a/object-named "cast")]
  (add-hovercast-open-listener cast-go
   (fn []
    (set-interactions (.IsOpen (a/cmpt cast-go HovercastInterface)))))))