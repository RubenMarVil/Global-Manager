;;;======================================================
;;;
;;;     CALCULATION OF THE USER LEVEL
;;;
;;;======================================================

;;*****************
;;*  MAIN MODULE  *
;;*****************

(defmodule MAIN (export ?ALL))

(deftemplate MAIN::attribute
    (slot name)
    (slot content)
)

(defrule MAIN::start
    (declare (salience 10000))
    =>
    (focus CALCULATE-LEVEL PROFILES)
)


;;****************************
;;*  CALCULATE-LEVEL MODULE  *
;;****************************

(defmodule CALCULATE-LEVEL (import MAIN ?ALL))


;;**************
;;*  PROFILES  *
;;**************

(defmodule PROFILES (import MAIN ?ALL))

(deffacts PROFILES::level-list
    (level (name "Low") (gsd-knowledge) (pm-knowledge) (cultural-knowledge) (language-knowledge) (time-knowledge))
    (level (name "Medium") (gsd-knowledge) (pm-knowledge) (cultural-knowledge) (language-knowledge) (time-knowledge))
    (level (name "High") (gsd-knowledge) (pm-knowledge) (cultural-knowledge) (language-knowledge) (time-knowledge))
)