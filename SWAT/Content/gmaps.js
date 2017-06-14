/**
 * DOMReady
 *
 * @fileOverview
 *    Cross browser object to attach functions that will be called
 *    immediatly when the DOM is ready.
 *    Released under MIT license.
 * @version 2.0.1
 * @author Victor Villaverde Laan
 * @link http://www.freelancephp.net/domready-javascript-object-cross-browser/
 * @link https://github.com/freelancephp/DOMReady
 */
(function (window) {

    /**
     * @namespace DOMReady
     */
    window.DOMReady = (function () {
        // Private vars
        var fns = [],
            isReady = false,
            //errorHandler = null,
            run = function (fn, args) {
                //try {
                // call function
                fn.apply(this, args || []);
                //} catch(err) {
                //	// error occured while executing function
                //	if (errorHandler)
                //		errorHandler.call(this, err);
                //}
            },
            ready = function () {
                isReady = true;

                // call all registered functions
                for (var x = 0; x < fns.length; x++)
                    run(fns[x].fn, fns[x].args || []);

                // clear handlers
                fns = [];
            };

        /**
         * Set error handler
         * @static
         * @param {Function} fn
         * @return {DOMReady} For chaining
         */
        //this.setOnError = function (fn) {
        //	errorHandler = fn;
        //
        //	// return this for chaining
        //	return this;
        //};

        /**
         * Add code or function to execute when the DOM is ready
         * @static
         * @param {Function} fn
         * @param {Array} args Arguments will be passed on when calling function
         * @return {DOMReady} For chaining
         */
        this.add = function (fn, args) {
            // call imediately when DOM is already ready
            if (isReady) {
                run(fn, args);
            } else {
                // add to the list
                fns[fns.length] = {
                    fn: fn,
                    args: args
                };
            }

            // return this for chaining
            return this;
        };

        if (document.readyState === "complete") {
            // everything is already loaded
            ready();
        } else if (window.addEventListener) {
            // for all browsers except IE<9
            window.document.addEventListener('DOMContentLoaded', function () { ready(); }, false);
        } else if (document.attachEvent) {
            // for IE
            // doScroll doesn't work properly in an iframe
            // original new code taken from http://javascript.info/tutorial/onload-ondomcontentloaded
            try {
                var isFrame = window.frameElement != null;
            } catch (e) { }

            if (document.documentElement.doScroll && !isFrame) {
                // IE, the document is not inside a frame
                (function () {
                    if (isReady) return;
                    try {
                        document.documentElement.doScroll("left");
                        ready();
                    } catch (e) {
                        setTimeout(arguments.callee, 10);
                    }
                })();
            } else {
                // IE, the document is inside a frame
                document.attachEvent("onreadystatechange", function () {
                    if (document.readyState === "complete") {
                        ready();
                    }
                })
            }

            /*
            // code taken from http://ajaxian.com/archives/iecontentloaded-yet-another-domcontentloaded
            (function(){
                // check IE's proprietary DOM members
                if (!window.document.uniqueID && window.document.expando)
                    return;
    
                // you can create any tagName, even customTag like <document :ready />
                var tempNode = window.document.createElement('document:ready');
    
                try {
                    // see if it throws errors until after ondocumentready
                    tempNode.doScroll('left');
    
                    // call ready
                    ready();
                } catch (err) {
                    setTimeout(arguments.callee, 10);
                }
            })();
            */
        }

        // Old browsers (and also as a fallback for if nothing else fires)
        if (window.addEventListener) {
            window.addEventListener('load', function () { ready(); }, false);
        } else if (window.attachEvent) {
            window.attachEvent('onload', function () { ready(); });
        } else {
            var fn = window.onload // very old browser, copy old onload
            window.onload = function () { // replace by new onload and call the old one
                if (fn) fn();
                ready();
            };
        }

        return this;

    })();

})(window);


if (typeof (mapClient) == "undefined") {
    var map, mapCenter, mapZoom, clusterer, infowindow, infowindowopen, mapClient, useCircleMarkers, mapMaxAutoZoom, imgBaseURL;
}
function initialize(elemId, useWIDEMaps, lat, lng, zoom, disableClustering, maxAutoZoom, useRoadmap) {
    if (useWIDEMaps && typeof (WIDEMaps) != "undefined") {
        mapClient = WIDEMaps;
    } else if (!useWIDEMaps && typeof (google) != "undefined" && google.maps) {
        mapClient = google.maps;
    }

    DOMReady.add(function () {
        DOMReadyInitialize(elemId, useWIDEMaps, lat, lng, zoom, disableClustering, maxAutoZoom, useRoadmap);
    });
}
function DOMReadyInitialize(elemId, useWIDEMaps, lat, lng, zoom, disableClustering, maxAutoZoom, useRoadmap) {
    if (!mapClient) {
        if (useWIDEMaps && typeof (WIDEMaps) != "undefined") {
            mapClient = WIDEMaps;
        } else if (!useWIDEMaps && typeof (google) != "undefined" && google.maps) {
            mapClient = google.maps;
        }
    }
    if (!mapClient || !mapClient.Map || !mapClient.LatLng || !mapClient.InfoWindow || !mapClient.event) {
        // who knows why this hasn't loaded yet, just keep on trying
        return setTimeout(function () { DOMReadyInitialize(elemId, useWIDEMaps, lat, lng, zoom, disableClustering, maxAutoZoom, useRoadmap); }, 500);
    }

    if (typeof (WIDEMaps) == 'undefined') {
        imgBaseURL = "http://www.comap.ca/widemaps/"
    } else {
        imgBaseURL = WIDEMaps.baseHref;
    }
    if (typeof (WIDEMaps) != 'undefined' && mapClient == WIDEMaps) {
        if (lat == undefined) lat = 49.069163;
        if (lng == undefined) lng = -83.910580;
        if (zoom == undefined) zoom = 0;
        if (maxAutoZoom == undefined) maxAutoZoom = 10;
        var myOptions = {
            maxAutoZoom: maxAutoZoom,
            mapTypeId: mapClient.MapTypeId.HYBRID
        };
    } else {
        if (lat == undefined) lat = 60;
        if (lng == undefined) lng = -95;
        if (zoom == undefined) zoom = 3;
        if (maxAutoZoom == undefined) maxAutoZoom = 17;
        var myOptions = {
            minZoom: 2,
            mapTypeId: (useRoadmap ? mapClient.MapTypeId.ROADMAP : mapClient.MapTypeId.HYBRID)
        };
    }

    mapMaxAutoZoom = parseInt(maxAutoZoom);
    mapCenter = new mapClient.LatLng(lat, lng);
    mapZoom = zoom;
    myOptions.center = mapCenter;
    myOptions.zoom = mapZoom;

    map = new mapClient.Map(document.getElementById(elemId), myOptions);
    if (typeof (MarkerClusterer) != 'undefined') {
        if (!disableClustering) {
            clusterer = new MarkerClusterer(map);
        } else {
            // if there are two maps, the last one initialized will get the points, which means if the cluster was already intialized for the first map, we need to disable it for the second map
            clusterer = null;
        }
    }

    infowindow = new mapClient.InfoWindow({
        //disableAutoPan:true
    });
    mapClient.event.addListener(infowindow, "closeclick", function () {
        infowindowopen = false;
        mapClient.event.trigger(map, "bounds_changed")
    });

    if (typeof (cometFrameID) != "undefined" && typeof (top.catchMapLocationEvent) != "undefined") {
        mapClient.event.addListener(map, "bounds_changed", function () {
            var currCenter = this.getCenter();
            var currZoom = this.getZoom();

            if (typeof (WIDEMaps) == 'undefined' || mapClient != WIDEMaps) {
                // adjust the Google zoom level to compensate for WIDEMaps difference (we do this because the panZoomToLocation call does the opposite)
                currZoom -= 5;
            }

            top.catchMapLocationEvent(
                cometFrameID,
                currZoom, currCenter.lng(), currCenter.lat(), 'GEODETIC'
            );
        });
    }

    window.svgMapFrameLoaded = true;
}

function mapGetRadians(x) {
    return x * Math.PI / 180;
}
function mapDistHaversine(p1, p2) {
    var R = 6371000; // earth's mean radius in metres
    var dLat = mapGetRadians(p2.lat() - p1.lat());
    var dLong = mapGetRadians(p2.lng() - p1.lng());

    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.cos(mapGetRadians(p1.lat())) * Math.cos(mapGetRadians(p2.lat())) * Math.sin(dLong / 2) * Math.sin(dLong / 2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c;

    return Math.round(d);
}
function mapDestinationPoint(lng1, lat1, dist, brng) {
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  */
    /*  Latitude/longitude spherical geodesy formulae & scripts (c) Chris Veness 2002-2011            */
    /*   - www.movable-type.co.uk/scripts/latlong.html                                                */
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  */
	/**
	 * Returns the destination point from this point having travelled the given distance (in km) on the
	 * given bearing along a rhumb line
	 *
	 * @param   {Number} brng: Bearing in degrees from North
	 * @param   {Number} dist: Distance in km
	 * @returns {LatLon} Destination point
	 */
    dist = parseFloat(dist);
    if (isNaN(dist)) dist = 0;

    brng = parseFloat(brng);
    if (isNaN(brng)) brng = 0;

    var d = dist / 6371;  // d = angular distance covered on earth's surface

    lat1 = lat1 * Math.PI / 180,
        lng1 = lng1 * Math.PI / 180;
    brng = brng * Math.PI / 180;

    var lat2 = lat1 + d * Math.cos(brng);
    var dLat = lat2 - lat1;
    var dPhi = Math.log(Math.tan(lat2 / 2 + Math.PI / 4) / Math.tan(lat1 / 2 + Math.PI / 4));
    var q = (!isNaN(dLat / dPhi)) ? dLat / dPhi : Math.cos(lat1);  // E-W line gives dPhi=0
    var dLng = d * Math.sin(brng) / q;
    // check for some daft bugger going past the pole
    if (Math.abs(lat2) > Math.PI / 2) lat2 = lat2 > 0 ? Math.PI - lat2 : -Math.PI - lat2;
    lng2 = (lng1 + dLng + 3 * Math.PI) % (2 * Math.PI) - Math.PI;

    lng2 = (lng2 / Math.PI * 180);
    lat2 = (lat2 / Math.PI * 180);

    return { X: lng2, Y: lat2 };
}

function loadDrawingManager(xColFieldName, yColFieldName, callback, noMarker, noCircle, noPolyline, noPolygon) {
    DOMReady.add(function () {
        DOMReadyLoadDrawingManager(xColFieldName, yColFieldName, callback, noMarker, noCircle, noPolyline, noPolygon);
    });
}
function DOMReadyLoadDrawingManager(xColFieldName, yColFieldName, callback, noMarker, noCircle, noPolyline, noPolygon) {
    if (typeof (svgMapFrameLoaded) == "undefined") {
        // who knows why this hasn't loaded yet, just keep on trying
        return setTimeout(function () { DOMReadyLoadDrawingManager(xColFieldName, yColFieldName, callback, noMarker, noCircle, noPolyline, noPolygon); }, 500);
    }
    var activeMap = map;
    var lastDrawnShape;
    var markerOptions = {
        draggable: true
    };
    if (useCircleMarkers) {
        markerOptions.icon = new mapClient.MarkerImage(
            imgBaseURL + "images/marker.php?S=2",
            new mapClient.Size(9, 9),
            new mapClient.Point(0, 0),
            new mapClient.Point(4, 4)
        );
    }

    if (xColFieldName != undefined && xColFieldName != '' && yColFieldName != undefined && yColFieldName != '') {
        // only draw points
        var xColField = document.getElementByIdOrName(xColFieldName);
        var yColField = document.getElementByIdOrName(yColFieldName);

        var drawingManager = new mapClient.drawing.DrawingManager({
            map: activeMap,
            drawingMode: mapClient.drawing.OverlayType.MARKER,
            drawingControlOptions: {
                position: mapClient.ControlPosition.TOP_CENTER,
                drawingModes: [
                    mapClient.drawing.OverlayType.MARKER
                ]
            },
            markerOptions: markerOptions
        });
        var markerDragendListener = function () {
            var markerPos = this.getPosition();

            xColField.value = markerPos.lng();
            yColField.value = markerPos.lat();

            if (callback) {
                callback(this, 'point');
            }

            if (typeof (cometFrameID) != "undefined" && typeof (top.catchMapPointEvent) != "undefined") {
                top.catchMapPointEvent(
                    cometFrameID,
                    xColField.value, yColField.value
                );
            }

            if (typeof (resetReCalcVisibleTimer) != "undefined") resetReCalcVisibleTimer(100);
            else if (typeof (reCalcVisible) != "undefined") reCalcVisible();
        };

        mapClient.event.addListener(drawingManager, 'overlaycomplete', function (event) {
            lastDrawnShape = destroyMapOverlay(lastDrawnShape);
            lastDrawnShape = event.overlay;

            if (event.type == mapClient.drawing.OverlayType.MARKER) {
                mapClient.event.addListener(lastDrawnShape, 'dragend', markerDragendListener);
                mapClient.event.trigger(lastDrawnShape, 'dragend');
            }

            // stop the drawing
            drawingManager.setDrawingMode(null);
        });

        // check for an existing shape
        window.set_x = function (newX, dontMove) {
            if (xColField.value != newX) xColField.value = newX;
            window.refreshDrawnPoint(true, dontMove);
        };
        window.set_y = function (newY, dontMove) {
            if (yColField.value != newY) yColField.value = newY;
            window.refreshDrawnPoint(true, dontMove);
        };
        window.set_x_y = function (newX, newY, dontMove) {
            if (xColField.value != newX) xColField.value = newX;
            if (yColField.value != newY) yColField.value = newY;
            window.refreshDrawnPoint(true, dontMove);
        };
        window.refreshDrawnPoint = function (reCenter, dontMove) {
            if (isFinite(xColField.value) && !isNaN(parseFloat(xColField.value)) && isFinite(yColField.value) && !isNaN(parseFloat(yColField.value))) {
                if (!lastDrawnShape) {
                    lastDrawnShape = new mapClient.Marker(markerOptions);
                    lastDrawnShape.setMap(activeMap);
                    mapClient.event.addListener(lastDrawnShape, 'dragend', markerDragendListener);
                }
                lastDrawnShape.setPosition(new mapClient.LatLng(yColField.value, xColField.value));

                drawingManager.setDrawingMode(null);

                if (!dontMove) {
                    if (reCenter) {
                        map.setCenter(lastDrawnShape.getPosition());
                    } else {
                        var existingShapeBounds = new mapClient.LatLngBounds();
                        existingShapeBounds.extend(lastDrawnShape.getPosition());
                        fitItemsInView(existingShapeBounds);
                    }
                }
            } else {
                lastDrawnShape = destroyMapOverlay(lastDrawnShape);
            }
        };
        window.refreshDrawnPoint();

        window.update_form_fields = function () {
        };
    } else {
        var existingShapeBounds = new mapClient.LatLngBounds();

        // all drawing types
        var drawing_type = document.getElementByIdOrName("type");
        var drawing_utmx = document.getElementByIdOrName("utmx");
        var drawing_utmy = document.getElementByIdOrName("utmy");
        var drawing_low_bound_x = document.getElementByIdOrName("low_bound_x");
        var drawing_low_bound_y = document.getElementByIdOrName("low_bound_y");
        var drawing_high_bound_x = document.getElementByIdOrName("high_bound_x");
        var drawing_high_bound_y = document.getElementByIdOrName("high_bound_y");
        var drawing_coords = document.getElementByIdOrName("coords");
        var drawing_centre = document.getElementByIdOrName("centre");
        var drawing_radius = document.getElementByIdOrName("radius");
        var drawing_zoom_level = document.getElementByIdOrName("zoom_level");
        var drawing_layer = document.getElementByIdOrName("layer");
        var drawing_stroke = document.getElementByIdOrName("stroke");
        var drawing_stroke_dash = document.getElementByIdOrName("stroke_dash");
        var drawing_stroke_width = document.getElementByIdOrName("stroke_width");
        var drawing_fill = document.getElementByIdOrName("fill");
        var drawing_opacity = document.getElementByIdOrName("opacity");

        var strokeColor = (drawing_stroke.tagName.toLowerCase() == 'select' ? drawing_stroke.options[drawing_stroke.selectedIndex].value : drawing_stroke.value);
        if (strokeColor.toLowerCase() == '_null_' || strokeColor == ' ') strokeColor = '';
        var strokeWeight = (drawing_stroke_width.tagName.toLowerCase() == 'select' ? drawing_stroke_width.options[drawing_stroke_width.selectedIndex].value : drawing_stroke_width.value);
        if (strokeWeight.toLowerCase() == '_null_' || strokeWeight == ' ') strokeWeight = '';
        var strokeDash = (drawing_stroke_dash.tagName.toLowerCase() == 'select' ? drawing_stroke_dash.options[drawing_stroke_dash.selectedIndex].value : drawing_stroke_dash.value);
        if (strokeDash.toLowerCase() == '_null_' || strokeDash == ' ') strokeDash = '';
        var fillColor = (drawing_fill.tagName.toLowerCase() == 'select' ? drawing_fill.options[drawing_fill.selectedIndex].value : drawing_fill.value);
        if (fillColor.toLowerCase() == '_null_' || fillColor == ' ') fillColor = '';

        var opacity = (drawing_opacity.tagName.toLowerCase() == 'select' ? drawing_opacity.options[drawing_opacity.selectedIndex].value : drawing_opacity.value);
        if (opacity.toLowerCase() == '_null_' || opacity == ' ') opacity = '';
        var strokeOpacity = opacity;
        var fillOpacity = opacity;

        if (fillColor.toLowerCase() == 'none') fillOpacity = '0';
        if (strokeColor.toLowerCase() == 'none') strokeOpacity = '0';

        var generalOverlayOptions = {
            strokeColor: strokeColor,
            strokeOpacity: strokeOpacity,
            strokeWeight: strokeWeight,
            strokeDash: strokeDash,
            fillColor: fillColor,
            fillOpacity: fillOpacity,
            editable: true
        };

        var drawingModes = [];
        if (!noMarker) drawingModes.push(mapClient.drawing.OverlayType.MARKER);
        if (!noCircle) drawingModes.push(mapClient.drawing.OverlayType.CIRCLE);
        if (!noPolyline) drawingModes.push(mapClient.drawing.OverlayType.POLYLINE);
        if (!noPolygon) drawingModes.push(mapClient.drawing.OverlayType.POLYGON);

        var drawingManager = new mapClient.drawing.DrawingManager({
            map: activeMap,
            drawingMode: mapClient.drawing.OverlayType.MARKER,
            drawingControlOptions: {
                position: mapClient.ControlPosition.TOP_CENTER,
                drawingModes: drawingModes
            },
            markerOptions: markerOptions,
            circleOptions: generalOverlayOptions,
            polylineOptions: generalOverlayOptions,
            polygonOptions: generalOverlayOptions
        });

        var shapeDoneRecorder = function () {
            if (typeof (cometFrameID) != "undefined" && typeof (top.catchMapDrawEvent) != "undefined") {
                top.catchMapDrawEvent(
                    cometFrameID,
                    drawing_type.value, drawing_utmx.value, drawing_utmy.value, drawing_coords.value, drawing_centre.value, drawing_radius.value
                );
            }
        }

        var markerDragendListener = function () {
            var markerPos = this.getPosition();

            drawing_utmx.value = markerPos.lng();
            drawing_utmy.value = markerPos.lat();

            drawing_low_bound_x.value = drawing_utmx.value;
            drawing_low_bound_y.value = drawing_utmy.value;
            drawing_high_bound_x.value = drawing_utmx.value;
            drawing_high_bound_y.value = drawing_utmy.value;

            drawing_zoom_level.value = activeMap.getZoom();

            drawing_type.value = 'point';
            drawing_coords.value = '';
            drawing_centre.value = '';
            drawing_radius.value = '';

            if (callback) {
                callback(this, 'point');
            }

            shapeDoneRecorder();

            if (typeof (resetReCalcVisibleTimer) != "undefined") resetReCalcVisibleTimer(100);
            else if (typeof (reCalcVisible) != "undefined") reCalcVisible();
            //alert(drawing_utmx.value+','+drawing_utmx.value+','+activeMap.getZoom());
        };
        var circleCenterChangedListener = function () {
            mapClient.event.trigger(this, 'radius_changed');
        };
        var circleRadiusChangedListener = function () {
            var circleBounds = this.getBounds();
            var NEPoint = circleBounds.getNorthEast();
            var SWPoint = circleBounds.getSouthWest();
            var circleCenter = this.getCenter();

            var radiusPoint = mapDestinationPoint(circleCenter.lng(), circleCenter.lat(), this.getRadius() / 1000, 90);

            drawing_centre.value = '' + circleCenter.lng() + ',' + circleCenter.lat();
            //drawing_radius.value = this.getRadius();
            drawing_radius.value = Math.abs(radiusPoint.X - circleCenter.lng())

            drawing_utmx.value = circleCenter.lng();
            drawing_utmy.value = circleCenter.lat();
            drawing_low_bound_x.value = Math.min(SWPoint.lng(), NEPoint.lng());
            drawing_low_bound_y.value = Math.min(SWPoint.lat(), NEPoint.lat());
            drawing_high_bound_x.value = Math.max(SWPoint.lng(), NEPoint.lng());
            drawing_high_bound_y.value = Math.max(SWPoint.lat(), NEPoint.lat());

            drawing_zoom_level.value = activeMap.getZoom();

            drawing_type.value = 'circle';
            drawing_coords.value = '';

            if (callback) {
                callback(this, 'circle');
            }

            shapeDoneRecorder();

            if (typeof (resetReCalcVisibleTimer) != "undefined") resetReCalcVisibleTimer(100);
            else if (typeof (reCalcVisible) != "undefined") reCalcVisible();
            //alert(drawing_centre.value+','+drawing_radius.value+','+activeMap.getZoom());
        };
        var polyInsertAtListener = function () {
            this.setAt(0, this.getAt(0));
        };
        var isPolygon = false;
        var polySetAtListener = function () {
            var polyArray = new Array();
            var polyMinX = this.getAt(0).lng();
            var polyMinY = this.getAt(0).lat();
            var polyMaxX = polyMinX;
            var polyMaxY = polyMinY;
            for (var i = 0; i < this.getLength(); i++) {
                var currPolyPoint = this.getAt(i);
                if (i == 0) {
                    var firstPolyPoint = currPolyPoint;
                } else if (i == this.getLength() - 1) {
                    var lastPolyPoint = currPolyPoint;
                }

                polyArray.push('' + currPolyPoint.lng() + ',' + currPolyPoint.lat());
                polyMinX = Math.min(polyMinX, currPolyPoint.lng());
                polyMinY = Math.min(polyMinY, currPolyPoint.lat());
                polyMaxX = Math.max(polyMaxX, currPolyPoint.lng());
                polyMaxY = Math.max(polyMaxY, currPolyPoint.lat());
            }
            if (isPolygon) {
                // if the polygon isn't closed, manually in the first point a second time to close it
                if (firstPolyPoint.lat() != lastPolyPoint.lat() || firstPolyPoint.lng() != lastPolyPoint.lng()) {
                    polyArray.push('' + firstPolyPoint.lng() + ',' + firstPolyPoint.lat());
                }
            }

            drawing_coords.value = polyArray.join(';');

            drawing_utmx.value = (polyMinX + polyMaxX) / 2;
            drawing_utmy.value = (polyMinY + polyMaxY) / 2;
            drawing_low_bound_x.value = polyMinX;
            drawing_low_bound_y.value = polyMinY;
            drawing_high_bound_x.value = polyMaxX;
            drawing_high_bound_y.value = polyMaxY;

            drawing_zoom_level.value = activeMap.getZoom();

            drawing_type.value = (isPolygon ? 'polygon' : 'polyline');
            drawing_centre.value = '';
            drawing_radius.value = '';

            if (callback) {
                callback(this, (isPolygon ? 'polygon' : 'polyline'));
            }

            shapeDoneRecorder();

            if (typeof (resetReCalcVisibleTimer) != "undefined") resetReCalcVisibleTimer(100);
            else if (typeof (reCalcVisible) != "undefined") reCalcVisible();
            //alert(drawing_coords.value+','+activeMap.getZoom());
        }

        mapClient.event.addListener(drawingManager, 'overlaycomplete', function (event) {
            lastDrawnShape = destroyMapOverlay(lastDrawnShape);
            lastDrawnShape = event.overlay;

            if (event.type == mapClient.drawing.OverlayType.MARKER) {
                mapClient.event.addListener(lastDrawnShape, 'dragend', markerDragendListener);
                mapClient.event.trigger(lastDrawnShape, 'dragend');
            } else if (event.type == mapClient.drawing.OverlayType.CIRCLE) {
                mapClient.event.addListener(lastDrawnShape, 'center_changed', circleCenterChangedListener);
                mapClient.event.addListener(lastDrawnShape, 'radius_changed', circleRadiusChangedListener);
                mapClient.event.trigger(lastDrawnShape, 'radius_changed');
            } else if (event.type == mapClient.drawing.OverlayType.POLYLINE || event.type == mapClient.drawing.OverlayType.POLYGON) {
                isPolygon = ((event.type == mapClient.drawing.OverlayType.POLYGON) ? true : false);
                var polyPath = lastDrawnShape.getPath();
                mapClient.event.addListener(polyPath, 'insert_at', polyInsertAtListener);
                mapClient.event.addListener(polyPath, 'remove_at', polyInsertAtListener);
                mapClient.event.addListener(polyPath, 'set_at', polySetAtListener);
                polyPath.setAt(0, polyPath.getAt(0));
            }

            // stop the drawing
            drawingManager.setDrawingMode(null);
        });

        // check for an existing shape
        window.refreshDrawnShape = function (dontRecord) {
            lastDrawnShape = destroyMapOverlay(lastDrawnShape);
            if (drawing_type.value == 'point') {
                if (isFinite(drawing_utmx.value) && !isNaN(parseFloat(drawing_utmx.value)) && isFinite(drawing_utmy.value) && !isNaN(parseFloat(drawing_utmy.value))) {
                    lastDrawnShape = new mapClient.Marker(markerOptions);
                    lastDrawnShape.setPosition(new mapClient.LatLng(drawing_utmy.value, drawing_utmx.value));
                    lastDrawnShape.setMap(activeMap);

                    if (!dontRecord) existingShapeBounds.extend(lastDrawnShape.getPosition());

                    mapClient.event.addListener(lastDrawnShape, 'dragend', markerDragendListener);
                    if (!dontRecord) mapClient.event.trigger(lastDrawnShape, 'dragend');
                }
            } else if (drawing_type.value == 'circle') {
                if (drawing_centre.value !== '' && isFinite(drawing_radius.value) && !isNaN(parseFloat(drawing_radius.value))) {
                    var currPointPair = drawing_centre.value.split(",");
                    if (isFinite(currPointPair[0]) && !isNaN(parseFloat(currPointPair[0])) && isFinite(currPointPair[1]) && !isNaN(parseFloat(currPointPair[1]))) {
                        var centrePoint = new mapClient.LatLng(parseFloat(currPointPair[1]), parseFloat(currPointPair[0]));
                        var radiusPoint = new mapClient.LatLng(parseFloat(currPointPair[1]), (parseFloat(currPointPair[0]) + parseFloat(drawing_radius.value)))

                        lastDrawnShape = new mapClient.Circle(generalOverlayOptions);
                        //lastDrawnShape.setCenter(new mapClient.LatLng(currPointPair[1],currPointPair[0]));
                        //lastDrawnShape.setRadius(Math.round(drawing_radius.value));
                        lastDrawnShape.setCenter(centrePoint);
                        lastDrawnShape.setRadius(mapDistHaversine(centrePoint, radiusPoint));
                        lastDrawnShape.setMap(activeMap);

                        if (!dontRecord) existingShapeBounds = lastDrawnShape.getBounds();

                        mapClient.event.addListener(lastDrawnShape, 'center_changed', circleCenterChangedListener);
                        mapClient.event.addListener(lastDrawnShape, 'radius_changed', circleRadiusChangedListener);
                        if (!dontRecord) mapClient.event.trigger(lastDrawnShape, 'radius_changed');
                    }
                }
            } else if (drawing_type.value == 'polyline' || drawing_type.value == 'polygon') {
                if (drawing_coords.value !== '') {
                    var pointArray = drawing_coords.value.split(";");
                    var usedPointArray = new Array();
                    for (var j = 0; j < pointArray.length; j++) {
                        var currPointPair = pointArray[j].split(",");
                        if (isFinite(currPointPair[0]) && !isNaN(parseFloat(currPointPair[0])) && isFinite(currPointPair[1]) && !isNaN(parseFloat(currPointPair[1]))) {
                            var newPoint = new mapClient.LatLng(currPointPair[1], currPointPair[0]);
                            usedPointArray.push(newPoint);
                            if (!dontRecord) existingShapeBounds.extend(newPoint);
                        }
                    }
                    if (usedPointArray.length > 0) {
                        if (drawing_type.value == 'polyline') {
                            isPolygon = false;
                            lastDrawnShape = new mapClient.Polyline(generalOverlayOptions);
                        } else {
                            isPolygon = true;
                            lastDrawnShape = new mapClient.Polygon(generalOverlayOptions);
                        }
                        lastDrawnShape.setPath(usedPointArray);
                        lastDrawnShape.setMap(activeMap);

                        var polyPath = lastDrawnShape.getPath();
                        mapClient.event.addListener(polyPath, 'insert_at', polyInsertAtListener);
                        mapClient.event.addListener(polyPath, 'remove_at', polyInsertAtListener);
                        mapClient.event.addListener(polyPath, 'set_at', polySetAtListener);
                        if (!dontRecord) polyPath.setAt(0, polyPath.getAt(0));
                    }
                }
            }
            if (lastDrawnShape) {
                drawingManager.setDrawingMode(null);
            }
        };
        window.refreshDrawnShape();

        if (lastDrawnShape) {
            fitItemsInView(existingShapeBounds);
        }


        window.set_draw_object = function (type, x, y, coords, centre, radius, dontRecord) {
            if (typeof (type) != "undefined") { drawing_type.value = type; }
            if (typeof (x) != "undefined") { drawing_utmx.value = x; };
            if (typeof (y) != "undefined") { drawing_utmy.value = y; };
            if (typeof (coords) != "undefined") { drawing_coords.value = coords; };
            if (typeof (centre) != "undefined") { drawing_centre.value = centre; };
            if (typeof (radius) != "undefined") { drawing_radius.value = radius; };

            window.refreshDrawnShape(dontRecord);
        };

        window.setDrawnAttribute = function (whichAttribute, newValue) {
            if (drawingManager && generalOverlayOptions) {
                if (newValue.toLowerCase() == '_null_' || newValue == ' ') newValue = '';
                generalOverlayOptions[whichAttribute] = newValue;
                drawingManager.setOptions({
                    circleOptions: generalOverlayOptions,
                    polylineOptions: generalOverlayOptions,
                    polygonOptions: generalOverlayOptions
                });
                if (lastDrawnShape) {
                    var changedOptions = {}
                    changedOptions[whichAttribute] = generalOverlayOptions[whichAttribute];
                    lastDrawnShape.setOptions(changedOptions);
                }
            }
        };

        window.set_stroke = function (newValue) {
            setDrawnAttribute('strokeColor', newValue);
            if (newValue.toLowerCase() == 'none') {
                setDrawnAttribute('strokeOpacity', '0');
            } else if (generalOverlayOptions['strokeOpacity'] != opacity) {
                setDrawnAttribute('strokeOpacity', opacity);
            }
        };
        window.set_stroke_width = function (newValue) {
            setDrawnAttribute('strokeWeight', newValue);
        };
        window.set_fill = function (newValue) {
            setDrawnAttribute('fillColor', newValue);
            if (newValue.toLowerCase() == 'none') {
                setDrawnAttribute('fillOpacity', '0');
            } else if (generalOverlayOptions['fillOpacity'] != opacity) {
                setDrawnAttribute('fillOpacity', opacity);
            }
        };
        window.set_opacity = function (newValue) {
            opacity = newValue;
            if (generalOverlayOptions['strokeColor'].toLowerCase() != 'none') {
                setDrawnAttribute('strokeOpacity', newValue);
            }
            if (generalOverlayOptions['fillColor'].toLowerCase() != 'none') {
                setDrawnAttribute('fillOpacity', newValue);
            }
        };
        window.set_stroke_dash = function (newValue) {
            setDrawnAttribute('strokeDash', newValue);
        };
        window.set_form = window.refreshDrawnShape;
    }
}

function destroyMapOverlay(overlay) {
    if (overlay) {
        overlay.setMap(null);
        mapClient.event.clearInstanceListeners(overlay);
        overlay = null;
    }
    return overlay;
}

function parseMapObjStyles(styleString) {
    var parsedStyles = new Array();
    if (styleString) {
        // split the styles up
        var currObjStyles = styleString.split(";");
        for (var k = 0; k < currObjStyles.length; k++) {
            // split the key and value up
            var currObjStylePart = currObjStyles[k].split(":");
            var currObjStylePartKey = currObjStylePart[0].replace(/^\s+|\s+$/g, "").toLowerCase();
            if (currObjStylePartKey != "") {
                // is this a supported style attribute?
                if (currObjStylePartKey == "stroke" ||
                    currObjStylePartKey == "fill" ||
                    currObjStylePartKey == "stroke-opacity" ||
                    currObjStylePartKey == "fill-opacity" ||
                    currObjStylePartKey == "stroke-dasharray" ||
                    currObjStylePartKey == "stroke-width" ||
                    currObjStylePartKey == "polyline"
                ) {
                    // yes it is, what is the value?
                    var currObjStylePartValue = currObjStylePart[1].replace(/^\s+|\s+$/g, "").split(/\!important/i)[0].replace(/^\s+|\s+$/g, "");
                    if (currObjStylePartValue !== "") {
                        // we have a good value, use it
                        parsedStyles[currObjStylePartKey] = currObjStylePartValue;
                    }
                }
            }
        }
    }
    return parsedStyles;
}
if (typeof (drawnAreas) == "undefined") {
    var drawnAreas = new Array();
    var drawnLocations = new Array();
    var drawnCircles = new Array();
    var drawnImages = new Array();
    var drawnAreaBounds = null;
    var drawnLocationBounds = null;
    var drawnImageBounds = null;
    var circleMarkers = new Array();
}
window.draw_areas = function (
    currMapAreaCoords,	// utms
    currMapAreaLabels,	// mouseover
    currMapAreaLinks,	// link
    currMapAreaClasses,	// class
    currMapAreaCoordTypes,	// coord type
    currMapAreaStyles,	// style
    currMapAreaDescrs	// infowindow
) {
    for (var k = 0; k < drawnAreas.length; k++) {
        drawnAreas[k] = destroyMapOverlay(drawnAreas[k]);
    }
    drawnAreas = new Array();
    drawnAreaBounds = new mapClient.LatLngBounds();

    currMapAreaDescrs = currMapAreaDescrs || new Array();
    var coordArray = typeof (currMapAreaCoords) != 'object' ? currMapAreaCoords.split(";;") : currMapAreaCoords;
    var labelArray = typeof (currMapAreaLabels) != 'object' ? currMapAreaLabels.split(";;") : currMapAreaLabels;
    var styleArray = typeof (currMapAreaStyles) != 'object' ? currMapAreaStyles.split("@@") : currMapAreaStyles;
    var descrArray = typeof (currMapAreaDescrs) != 'object' ? currMapAreaDescrs.split(";;") : currMapAreaDescrs;
    var linkArray = typeof (currMapAreaLinks) != 'object' ? currMapAreaLinks.split(";;") : currMapAreaLinks;
    for (var i = (coordArray.length - 1); i >= 0; i--) {
        if (coordArray[i] !== "") {
            var pointArray = coordArray[i].split(";");
            var usedPointArray = new Array();
            for (var j = 0; j < pointArray.length; j++) {
                var currPointPair = pointArray[j].split(",");
                if (isFinite(currPointPair[0]) && !isNaN(parseFloat(currPointPair[0])) && isFinite(currPointPair[1]) && !isNaN(parseFloat(currPointPair[1]))) {
                    var newPoint = new mapClient.LatLng(currPointPair[1], currPointPair[0]);
                    usedPointArray.push(newPoint);
                    drawnAreaBounds.extend(newPoint);
                }
            }
            if (usedPointArray.length > 0) {
                var title = '';
                if (labelArray[i] && labelArray[i].indexOf("<") == -1 && typeof (WIDEMaps) != 'undefined' && mapClient == WIDEMaps) {
                    title = labelArray[i];
                    labelArray[i] = '';
                }
                var passedStyles = parseMapObjStyles(styleArray[i]);
                var newArea = new mapClient[(passedStyles["polyline"] ? 'Polyline' : 'Polygon')]({
                    path: usedPointArray,
                    strokeColor: (passedStyles["stroke"] != undefined ? passedStyles["stroke"] : "#000000"),
                    strokeOpacity: (passedStyles["stroke"] != undefined && passedStyles["stroke"].toLowerCase() == 'none' ? 0 : (passedStyles["stroke-opacity"] != undefined ? passedStyles["stroke-opacity"] : 0.6)),
                    strokeWeight: (passedStyles["stroke-width"] != undefined ? passedStyles["stroke-width"] : 2),
                    fillColor: (passedStyles["fill"] != undefined ? passedStyles["fill"] : "Orange"),
                    fillOpacity: (passedStyles["fill"] != undefined && passedStyles["fill"].toLowerCase() == 'none' ? 0 : (passedStyles["fill-opacity"] != undefined ? passedStyles["fill-opacity"] : 0.6)),
                    strokeDash: (passedStyles["stroke-dasharray"] != undefined ? passedStyles["stroke-dasharray"] : "none"),
                    map: map,
                    title: title
                });
                if (labelArray[i] || descrArray[i] || linkArray[i]) {
                    addClickListener(newArea, labelArray[i], descrArray[i], linkArray[i]);
                }

                drawnAreas.push(newArea);
            }
        }
    }
};

window.draw_locations = function (
    currMapUTMs,		// utms
    currMapLabels,		// mouseover
    currMapLinks,		// link
    currMapClasses,		// class
    currMapCoordTypes,	// coord type
    currMapStyles,		// style
    currMapRadii,		// radius
    currMapDescrs		// infowindow
) {
    if (clusterer) {
        clusterer.removeMarkers(drawnLocations);
    }
    for (var k = 0; k < drawnLocations.length; k++) {
        drawnLocations[k] = destroyMapOverlay(drawnLocations[k]);
    }
    drawnLocations = new Array();
    for (var k = 0; k < drawnCircles.length; k++) {
        drawnCircles[k] = destroyMapOverlay(drawnCircles[k]);
    }
    drawnCircles = new Array();
    drawnLocationBounds = new mapClient.LatLngBounds();

    currMapDescrs = currMapDescrs || new Array();
    var coordArray = typeof (currMapUTMs) != 'object' ? currMapUTMs.split(";;") : currMapUTMs;
    var labelArray = typeof (currMapLabels) != 'object' ? currMapLabels.split(";;") : currMapLabels;
    var descrArray = typeof (currMapDescrs) != 'object' ? currMapDescrs.split(";;") : currMapDescrs;
    var linkArray = typeof (currMapLinks) != 'object' ? currMapLinks.split(";;") : currMapLinks;
    var classArray = typeof (currMapClasses) != 'object' ? currMapClasses.split(";;") : currMapClasses;
    var radiusArray = typeof (currMapRadii) != 'object' ? currMapRadii.split(";;") : currMapRadii;
    var styleArray = typeof (currMapStyles) != 'object' ? currMapStyles.split("@@") : currMapStyles;
    for (var i = (coordArray.length - 1); i >= 0; i--) {
        if (coordArray[i] !== "") {
            var currPointPair = coordArray[i].split(",");
            if (isFinite(currPointPair[0]) && !isNaN(parseFloat(currPointPair[0])) && isFinite(currPointPair[1]) && !isNaN(parseFloat(currPointPair[1]))) {
                currPointPair = new mapClient.LatLng(currPointPair[1], currPointPair[0]);
                var title = '';
                if (labelArray[i] && labelArray[i].indexOf("<") == -1) {
                    title = labelArray[i];
                    labelArray[i] = '';
                }
                if (radiusArray[i] !== undefined && radiusArray[i] !== null && isFinite(radiusArray[i]) && !isNaN(parseFloat(radiusArray[i])) && parseFloat(radiusArray[i]) > 0) {
                    // this is a circle
                    var radiusPoint = new mapClient.LatLng(currPointPair.lat(), (currPointPair.lng() + parseFloat(radiusArray[i])))

                    var passedStyles = parseMapObjStyles(styleArray[i]);
                    var newMarker = new mapClient.Circle({
                        center: currPointPair,
                        radius: mapDistHaversine(currPointPair, radiusPoint),
                        strokeColor: (passedStyles["stroke"] != undefined ? passedStyles["stroke"] : "#000000"),
                        strokeOpacity: (passedStyles["stroke"] != undefined && passedStyles["stroke"].toLowerCase() == 'none' ? 0 : (passedStyles["stroke-opacity"] != undefined ? passedStyles["stroke-opacity"] : 0.6)),
                        strokeWeight: (passedStyles["stroke-width"] != undefined ? passedStyles["stroke-width"] : 2),
                        fillColor: (passedStyles["fill"] != undefined ? passedStyles["fill"] : "Orange"),
                        fillOpacity: (passedStyles["fill"] != undefined && passedStyles["fill"].toLowerCase() == 'none' ? 0 : (passedStyles["fill-opacity"] != undefined ? passedStyles["fill-opacity"] : 0.6)),
                        strokeDash: (passedStyles["stroke-dasharray"] != undefined ? passedStyles["stroke-dasharray"] : "none"),
                        map: map,
                        title: title
                    });
                    drawnCircles.push(newMarker);

                    var circleBounds = newMarker.getBounds();
                    drawnLocationBounds.extend(circleBounds.getNorthEast());
                    drawnLocationBounds.extend(circleBounds.getSouthWest());
                } else {
                    // this is a marker
                    if (classArray[i] === undefined || classArray[i] === null) classArray[i] = '';
                    if (useCircleMarkers) {
                        if (circleMarkers[classArray[i]] == undefined) {
                            circleMarkers[classArray[i]] = new mapClient.MarkerImage(
                                imgBaseURL + "images/marker.php?C=" + encodeURIComponent(classArray[i]) + "&S=2",
                                new mapClient.Size(9, 9),
                                new mapClient.Point(0, 0),
                                new mapClient.Point(4, 4)
                            );
                        }
                        var currIcon = circleMarkers[classArray[i]];
                    } else {
                        var currIcon = imgBaseURL + "images/marker.php?C=" + encodeURIComponent(classArray[i]);
                    }
                    var newMarker = new mapClient.Marker({
                        position: currPointPair,
                        icon: currIcon,
                        map: (clusterer ? null : map),
                        title: title
                    });
                    drawnLocations.push(newMarker);

                    drawnLocationBounds.extend(currPointPair);
                }
                if (labelArray[i] || descrArray[i] || linkArray[i]) {
                    addClickListener(newMarker, labelArray[i], descrArray[i], linkArray[i]);
                }
            }
        }
    }
    if (clusterer) {
        clusterer.addMarkers(drawnLocations);
    }
};

window.draw_images = function (
    currMapImageUTMs,	// utms
    currMapImageFiles,	// images
    currMapImageWidths,	// image widths
    currMapImageHeights,	// image heights
    currMapImageLabels,	// mouseover
    currMapImageLinks,	// link
    currMapImageClasses,	// class
    currMapImageCoordTypes,	// coord type
    currMapImageStyles,	// style
    currMapImageDescrs	// infowindow
) {
    if (clusterer) {
        clusterer.removeMarkers(drawnImages);
    }
    for (var k = 0; k < drawnImages.length; k++) {
        drawnImages[k] = destroyMapOverlay(drawnImages[k]);
    }
    drawnImages = new Array();
    drawnImageBounds = new mapClient.LatLngBounds();

    currMapImageDescrs = currMapImageDescrs || new Array();
    var coordArray = typeof (currMapImageUTMs) != 'object' ? currMapImageUTMs.split(";;") : currMapImageUTMs;
    var labelArray = typeof (currMapImageLabels) != 'object' ? currMapImageLabels.split(";;") : currMapImageLabels;
    var descrArray = typeof (currMapImageDescrs) != 'object' ? currMapImageDescrs.split(";;") : currMapImageDescrs;
    var linkArray = typeof (currMapImageLinks) != 'object' ? currMapImageLinks.split(";;") : currMapImageLinks;
    var filesArray = typeof (currMapImageFiles) != 'object' ? currMapImageFiles.split(";;") : currMapImageFiles;
    for (var i = (coordArray.length - 1); i >= 0; i--) {
        if (coordArray[i] !== "") {
            var currPointPair = coordArray[i].split(",");
            if (isFinite(currPointPair[0]) && !isNaN(parseFloat(currPointPair[0])) && isFinite(currPointPair[1]) && !isNaN(parseFloat(currPointPair[1]))) {
                currPointPair = new mapClient.LatLng(currPointPair[1], currPointPair[0]);
                drawnImageBounds.extend(currPointPair);
                var title = '';
                if (labelArray[i] && labelArray[i].indexOf("<") == -1) {
                    title = labelArray[i];
                    labelArray[i] = '';
                }
                var newMarker = new mapClient.Marker({ position: currPointPair, map: (clusterer ? null : map), title: title, icon: filesArray[i].replace("../", "") });
                if (labelArray[i] || descrArray[i] || linkArray[i]) {
                    addClickListener(newMarker, labelArray[i], descrArray[i], linkArray[i]);
                }
                drawnImages.push(newMarker);
            }
        }
    }
    if (clusterer) {
        clusterer.addMarkers(drawnImages);
    }
};

window.panZoomToLocation = function (locationZoom, locationX, locationY, locationCoordType, locationLabel, utmZone, ignoreMaxZoom) {
    if (typeof (svgMapFrameLoaded) == "undefined") {
        // who knows why this hasn't loaded yet, just keep on trying
        return setTimeout(function () { panZoomToLocation(locationZoom, locationX, locationY, locationCoordType, locationLabel, utmZone); }, 500);
    }

    if ((typeof (WIDEMaps) == 'undefined' || mapClient != WIDEMaps) && locationCoordType != undefined && locationCoordType != '' && locationCoordType.toLowerCase() != 'geodetic') {
        alert('Only Lat/Long coordinates are currently supported.');
    } else {
        if (typeof (WIDEMaps) != 'undefined' && mapClient == WIDEMaps && locationCoordType != undefined && locationCoordType != '' && locationCoordType.toLowerCase() != 'geodetic') {
            var newLocation = new mapClient.LatLng(locationX, locationY, locationCoordType, false, utmZone);
        } else {
            var newLocation = new mapClient.LatLng(locationY, locationX);
        }

        var newZoomLevel = mapMaxAutoZoom;
        if (locationZoom !== undefined && locationZoom !== null && locationZoom !== false) {
            locationZoom = parseInt(locationZoom);
            if (typeof (WIDEMaps) == 'undefined' || mapClient != WIDEMaps) {
                // adjust the Google zoom level to compensate for WIDEMaps difference
                locationZoom += 5;
            }

            if (ignoreMaxZoom || locationZoom < mapMaxAutoZoom) {
                newZoomLevel = locationZoom;
            }
        }

        map.setCenter(newLocation);
        map.setZoom(newZoomLevel);
    }
};

window.redrawMapItems = function (areaPrefix, objPrefix, imgPrefix) {
    if (typeof (svgMapFrameLoaded) == "undefined") return startMapTimer(function () { redrawMapItems(areaPrefix, objPrefix, imgPrefix); });

    if (infowindowopen) {
        // close the open window
        infowindowopen = false;
        infowindow.close();
    }

    hideMapWarning();
    mapClient.event.trigger(map, 'resize');

    draw_areas(
        window[areaPrefix + 'UTMs'] || new Array(),
        window[areaPrefix + 'Labels'] || new Array(),
        window[areaPrefix + 'Links'] || new Array(),
        window[areaPrefix + 'Classes'] || new Array(),
        window[areaPrefix + 'CoordTypes'] || new Array(),
        window[areaPrefix + 'Styles'] || new Array(),
        window[areaPrefix + 'Descriptions'] || new Array()
    );
    draw_locations(
        window[objPrefix + 'UTMs'] || new Array(),
        window[objPrefix + 'Labels'] || new Array(),
        window[objPrefix + 'Links'] || new Array(),
        window[objPrefix + 'Classes'] || new Array(),
        window[objPrefix + 'CoordTypes'] || new Array(),
        window[objPrefix + 'Styles'] || new Array(),
        window[objPrefix + 'Radii'] || new Array(),
        window[objPrefix + 'Descriptions'] || new Array()
    );
    draw_images(
        window[imgPrefix + 'UTMs'] || new Array(),
        window[imgPrefix + 'Files'] || new Array(),
        window[imgPrefix + 'Widths'] || new Array(),
        window[imgPrefix + 'Heights'] || new Array(),
        window[imgPrefix + 'Labels'] || new Array(),
        window[imgPrefix + 'Links'] || new Array(),
        window[imgPrefix + 'Classes'] || new Array(),
        window[imgPrefix + 'CoordTypes'] || new Array(),
        window[imgPrefix + 'Styles'] || new Array(),
        window[imgPrefix + 'Descriptions'] || new Array()
    );

    fitItemsInView();
};
window.showstationsOnMap = function () {
    redrawMapItems('mappedArea', 'mappedObject', 'mappedImage');
};
window.showstationsSpecificOnMap = function () {
    redrawMapItems('mappedAreaSpecific', 'mappedObjectSpecific', 'mappedImageSpecific');
};
window.showstationOnMap = function (x, y, label, link, theClass, coordType, theStyle, radius, descr) {
    window['showstationUTMs'] = new Array(x + "," + y);
    window['showstationLabels'] = new Array((label ? "" + label : ''));
    window['showstationLinks'] = new Array((link ? "" + link : ''));
    window['showstationClasses'] = new Array((theClass ? "" + theClass : ''));
    window['showstationStyles'] = new Array((theStyle ? "" + theStyle : ''));
    window['showstationCoordTypes'] = new Array((coordType ? "" + coordType : ''));
    window['showstationDescriptions'] = new Array((descr ? "" + descr : ''));
    window['showstationRadii'] = new Array((radius != undefined ? "" + radius : ''));

    redrawMapItems('NOAREAS', 'showstation', 'NOIMAGES');
};
window.showImageOnMap = function (x, y, file, width, height, label, link, theClass, coordType, theStyle, descr) {
    window['showImageUTMs'] = new Array(x + "," + y);
    window['showImageLabels'] = new Array((label ? "" + label : ''));
    window['showImageLinks'] = new Array((link ? "" + link : ''));
    window['showImageClasses'] = new Array((theClass ? "" + theClass : ''));
    window['showImageStyles'] = new Array((theStyle ? "" + theStyle : ''));
    window['showImageCoordTypes'] = new Array((coordType ? "" + coordType : ''));
    window['showImageDescriptions'] = new Array((descr ? "" + descr : ''));
    window['showImageFiles'] = new Array((file ? "" + file : ''));
    window['showImageWidths'] = new Array((width ? "" + width : ''));
    window['showImageHeights'] = new Array((height ? "" + height : ''));

    redrawMapItems('NOAREAS', 'NOOBJECTS', 'showImage');
};
window.showAreaOnMap = function (coords, label, link, theClass, coordType, theStyle, descr) {
    window['showAreaUTMs'] = new Array(coords);
    window['showAreaLabels'] = new Array((label ? "" + label : ''));
    window['showAreaLinks'] = new Array((link ? "" + link : ''));
    window['showAreaClasses'] = new Array((theClass ? "" + theClass : ''));
    window['showAreaStyles'] = new Array((theStyle ? "" + theStyle : ''));
    window['showAreaCoordTypes'] = new Array((coordType ? "" + coordType : ''));
    window['showAreaDescriptions'] = new Array((descr ? "" + descr : ''));

    redrawMapItems('showArea', 'NOOBJECTS', 'NOIMAGES');
}

if (typeof (mapTimerGoing) == "undefined") var mapTimerGoing;
if (typeof (mapTimedWindow) == "undefined") var mapTimedWindow;
function startMapTimer(whichFunction) {
    var warning = document.getElementById("warning");
    if (warning) {
        warning.style.position = "relative";
        warning.style.visibility = "visible";
    }
    if (mapTimerGoing) {
        clearTimeout(mapTimedWindow);
    }
    mapTimerGoing = true;
    mapTimedWindow = setTimeout(whichFunction, 500);
}

function hideMapWarning() {
    var ifrm = document.getElementById("ap_iframe");
    if (ifrm) {
        var currStyle = (window.getComputedStyle ? window.getComputedStyle(ifrm, null) : false) || ifrm.currentStyle || ifrm.style;
        var doResize = false;

        var currWidth = parseFloat(currStyle.width);
        if (!isNaN(currWidth) && currWidth <= 0) {
            ifrm.style.width = "100%";
            doResize = true;
        }

        var currHeight = parseFloat(currStyle.height);
        if (!isNaN(currHeight) && currHeight <= 0) {
            ifrm.style.height = "500px";
            doResize = true;
        }

        if (doResize) {
            mapClient.event.trigger(map, 'resize');
        }
    }

    var warning = document.getElementById("warning");
    if (warning) {
        warning.style.visibility = "hidden";
        warning.style.position = "absolute";
    }
}

function fitItemsInView(drawnItemBounds) {
    if (!drawnItemBounds) {
        var drawnItemBounds = new mapClient.LatLngBounds();
        if (drawnAreaBounds && !drawnAreaBounds.isEmpty()) {
            drawnItemBounds.extend(drawnAreaBounds.getNorthEast());
            drawnItemBounds.extend(drawnAreaBounds.getSouthWest());
        }
        if (drawnLocationBounds && !drawnLocationBounds.isEmpty()) {
            drawnItemBounds.extend(drawnLocationBounds.getNorthEast());
            drawnItemBounds.extend(drawnLocationBounds.getSouthWest());
        }
        if (drawnImageBounds && !drawnImageBounds.isEmpty()) {
            drawnItemBounds.extend(drawnImageBounds.getNorthEast());
            drawnItemBounds.extend(drawnImageBounds.getSouthWest());
        }
    }

    if (!drawnItemBounds.isEmpty()) {
        // make sure we don't zoom too far in when setting the bounds
        if (typeof (WIDEMaps) == 'undefined' || mapClient != WIDEMaps) {
            zoomChangeBoundsListener = mapClient.event.addListenerOnce(map, 'bounds_changed', function (event) {
                if (this.getZoom() > mapMaxAutoZoom) {
                    this.setZoom(mapMaxAutoZoom);
                }
            });
            setTimeout(function () { mapClient.event.removeListener(zoomChangeBoundsListener) }, 2000);
        }
        map.fitBounds(drawnItemBounds);
    }
}

function addClickListener(marker, label, descr, link) {
    var infowindowContent = descr || label.replace("../", "");

    if (typeof (cometFrameID) != "undefined") {
        // this is a collaboration map, let's double check and make sure that we don't have any crazy iframes
        infowindowContent = infowindowContent.replace(/(<\s*iframe[^>]*\ssrc\s*=\s*"?[^">]+\?[^">]*)("?)/gi, "$1&amp;cometMapFrame$2");
    }

    if (infowindowContent) {
        if (link) {
            infowindowContent += '<p><small><a href="' + link.replace("../../", "") + '" target="_blank">More details</a></small></p>';
        }
        mapClient.event.addListener(marker, "click", function () {
            if (typeof (clearReCalcVisibleTimer) != "undefined") clearReCalcVisibleTimer();
            infowindowopen = true;
            infowindow.setContent(infowindowContent);
            infowindow.open(map, marker);
        });
    } else if (link) {
        // just open a link
        mapClient.event.addListener(marker, "click", function () {
            window.open(link.replace("../../", ""));
        });
    }
}

function loadSearchCapabilities(listenForBoundsChange) {
    DOMReady.add(function () {
        DOMReadyLoadSearchCapabilities(listenForBoundsChange);
    });
}
function DOMReadyLoadSearchCapabilities(listenForBoundsChange) {
    if (typeof (svgMapFrameLoaded) == "undefined") {
        // who knows why this hasn't loaded yet, just keep on trying
        return setTimeout(function () { DOMReadyLoadSearchCapabilities(listenForBoundsChange); }, 500);
    }
    var activeMap = map;
    var searchType = document.getElementByIdOrName("searchType");

    var searchCenterX = document.getElementByIdOrName("searchCenterX");
    var searchCenterY = document.getElementByIdOrName("searchCenterY");
    var searchPointX = document.getElementByIdOrName("searchPointX");
    var searchPointY = document.getElementByIdOrName("searchPointY");
    var searchRadius = document.getElementByIdOrName("searchRadius");

    var X1 = document.getElementByIdOrName("X1");
    var X2 = document.getElementByIdOrName("X2");
    var Y1 = document.getElementByIdOrName("Y1");
    var Y2 = document.getElementByIdOrName("Y2");

    var lastDrawnSearchShape;
    var existingSearchShapeBounds = new mapClient.LatLngBounds();

    var generalOverlayOptions = {
        strokeColor: '#000000',
        strokeOpacity: 1,
        strokeWeight: 2,
        fillColor: '#ffff00',
        fillOpacity: 0.2,
        editable: true
    };
    var drawingManager = new mapClient.drawing.DrawingManager({
        map: activeMap,
        drawingMode: null,
        drawingControlOptions: {
            position: mapClient.ControlPosition.TOP_CENTER,
            drawingModes: [
                mapClient.drawing.OverlayType.CIRCLE,
                mapClient.drawing.OverlayType.RECTANGLE,
                mapClient.drawing.OverlayType.POLYGON
            ]
        },
        circleOptions: generalOverlayOptions,
        rectangleOptions: generalOverlayOptions,
        polygonOptions: generalOverlayOptions
    });

    var searchDoneRecorder = function () {
        if (typeof (cometFrameID) != "undefined" && typeof (top.catchMapSearchEvent) != "undefined") {
            top.catchMapSearchEvent(
                cometFrameID,
                searchType.value, searchPointX.value, searchPointY.value, searchCenterX.value, searchCenterY.value, searchRadius.value, X1.value, X2.value, Y1.value, Y2.value
            );
        }
    };

    var circleCenterChangedListener = function () {
        mapClient.event.trigger(this, 'radius_changed');
    };
    var circleRadiusChangedListener = function () {
        var circleBounds = this.getBounds();
        var NEPoint = circleBounds.getNorthEast();
        var SWPoint = circleBounds.getSouthWest();
        var circleCenter = this.getCenter();

        var radiusPoint = mapDestinationPoint(circleCenter.lng(), circleCenter.lat(), this.getRadius() / 1000, 90);

        X1.value = Math.min(SWPoint.lng(), NEPoint.lng());
        X2.value = Math.max(SWPoint.lng(), NEPoint.lng());
        Y1.value = Math.max(SWPoint.lat(), NEPoint.lat());
        Y2.value = Math.min(SWPoint.lat(), NEPoint.lat());

        searchCenterX.value = circleCenter.lng();
        searchCenterY.value = circleCenter.lat();

        //searchRadius.value = this.getRadius();
        searchRadius.value = Math.abs(radiusPoint.X - circleCenter.lng())

        searchType.value = 'radius';
        searchPointX.value = '';
        searchPointY.value = '';

        searchDoneRecorder();

        if (typeof (resetReCalcVisibleTimer) != "undefined") resetReCalcVisibleTimer(100);
        else if (typeof (reCalcVisible) != "undefined") reCalcVisible();
    };
    var rectangleBoundsChangedListener = function () {
        var rectangleBounds = this.getBounds();
        var NEPoint = rectangleBounds.getNorthEast();
        var SWPoint = rectangleBounds.getSouthWest();
        X1.value = Math.min(SWPoint.lng(), NEPoint.lng());
        X2.value = Math.max(SWPoint.lng(), NEPoint.lng());
        Y1.value = Math.max(SWPoint.lat(), NEPoint.lat());
        Y2.value = Math.min(SWPoint.lat(), NEPoint.lat());

        searchPointX.value = '' + SWPoint.lng() + ',' + NEPoint.lng() + ',' + NEPoint.lng() + ',' + SWPoint.lng();
        searchPointY.value = '' + SWPoint.lat() + ',' + NEPoint.lat() + ',' + NEPoint.lat() + ',' + SWPoint.lat();

        searchType.value = 'rect';
        searchCenterX.value = '';
        searchCenterY.value = '';
        searchRadius.value = '';

        searchDoneRecorder();

        if (typeof (resetReCalcVisibleTimer) != "undefined") resetReCalcVisibleTimer(100);
        else if (typeof (reCalcVisible) != "undefined") reCalcVisible();
    };
    var polyInsertAtListener = function () {
        this.setAt(0, this.getAt(0));
    };
    var polySetAtListener = function () {
        var polyXArray = new Array();
        var polyYArray = new Array();
        var polyMinX = this.getAt(0).lng();
        var polyMinY = this.getAt(0).lat();
        var polyMaxX = polyMinX;
        var polyMaxY = polyMinY;
        for (var i = 0; i < this.getLength(); i++) {
            var currPolyPoint = this.getAt(i);
            if (i == 0) {
                var firstPolyPoint = currPolyPoint;
            } else if (i == this.getLength() - 1) {
                var lastPolyPoint = currPolyPoint;
            }
            polyXArray.push('' + currPolyPoint.lng());
            polyYArray.push('' + currPolyPoint.lat());
            polyMinX = Math.min(polyMinX, currPolyPoint.lng());
            polyMinY = Math.min(polyMinY, currPolyPoint.lat());
            polyMaxX = Math.max(polyMaxX, currPolyPoint.lng());
            polyMaxY = Math.max(polyMaxY, currPolyPoint.lat());
        }
        // if the polygon isn't closed, manually in the first point a second time to close it
        if (firstPolyPoint.lat() != lastPolyPoint.lat() || firstPolyPoint.lng() != lastPolyPoint.lng()) {
            polyXArray.push('' + firstPolyPoint.lng());
            polyYArray.push('' + firstPolyPoint.lat());
        }

        searchPointX.value = polyXArray.join(',');
        searchPointY.value = polyYArray.join(',');

        X1.value = polyMinX;
        X2.value = polyMaxX;
        Y1.value = polyMaxY;
        Y2.value = polyMinY;

        searchType.value = 'poly';
        searchCenterX.value = '';
        searchCenterY.value = '';
        searchRadius.value = '';

        searchDoneRecorder();

        if (typeof (resetReCalcVisibleTimer) != "undefined") resetReCalcVisibleTimer(100);
        else if (typeof (reCalcVisible) != "undefined") reCalcVisible();
    }

    mapClient.event.addListener(drawingManager, 'overlaycomplete', function (event) {
        lastDrawnSearchShape = destroyMapOverlay(lastDrawnSearchShape);
        lastDrawnSearchShape = event.overlay;

        if (event.type == mapClient.drawing.OverlayType.CIRCLE) {
            mapClient.event.addListener(lastDrawnSearchShape, 'center_changed', circleCenterChangedListener);
            mapClient.event.addListener(lastDrawnSearchShape, 'radius_changed', circleRadiusChangedListener);
            mapClient.event.trigger(lastDrawnSearchShape, 'radius_changed');
        } else if (event.type == mapClient.drawing.OverlayType.RECTANGLE) {
            mapClient.event.addListener(lastDrawnSearchShape, 'bounds_changed', rectangleBoundsChangedListener);
            mapClient.event.trigger(lastDrawnSearchShape, 'bounds_changed');
        } else if (event.type == mapClient.drawing.OverlayType.POLYGON) {
            var polyPath = lastDrawnSearchShape.getPath();
            mapClient.event.addListener(polyPath, 'insert_at', polyInsertAtListener);
            mapClient.event.addListener(polyPath, 'remove_at', polyInsertAtListener);
            mapClient.event.addListener(polyPath, 'set_at', polySetAtListener);
            polyPath.setAt(0, polyPath.getAt(0));
        }

        if (!listenForBoundsChange) drawingManager.setDrawingMode(null);
    });

    // check for an existing shape
    window.refreshDrawnSearch = function (dontRecord) {
        lastDrawnSearchShape = destroyMapOverlay(lastDrawnSearchShape);
        if (searchType.value == 'radius') {
            if (isFinite(searchCenterX.value) && !isNaN(parseFloat(searchCenterX.value)) && isFinite(searchCenterY.value) && !isNaN(parseFloat(searchCenterY.value)) && isFinite(searchRadius.value) && !isNaN(parseFloat(searchRadius.value))) {
                var centrePoint = new mapClient.LatLng(parseFloat(searchCenterY.value), parseFloat(searchCenterX.value));
                var radiusPoint = new mapClient.LatLng(parseFloat(searchCenterY.value), (parseFloat(searchCenterX.value) + parseFloat(searchRadius.value)))

                lastDrawnSearchShape = new mapClient.Circle(generalOverlayOptions);
                lastDrawnSearchShape.setCenter(centrePoint);
                lastDrawnSearchShape.setRadius(mapDistHaversine(centrePoint, radiusPoint));
                lastDrawnSearchShape.setMap(activeMap);

                existingSearchShapeBounds = lastDrawnSearchShape.getBounds();

                mapClient.event.addListener(lastDrawnSearchShape, 'center_changed', circleCenterChangedListener);
                mapClient.event.addListener(lastDrawnSearchShape, 'radius_changed', circleRadiusChangedListener);
                if (!dontRecord) mapClient.event.trigger(lastDrawnSearchShape, 'radius_changed');

                if (listenForBoundsChange) drawingManager.setDrawingMode(mapClient.drawing.OverlayType.CIRCLE);
            }
        } else if (searchType.value == 'rect') {
            if (searchPointX.value !== '' && searchPointY.value !== '') {
                var rectPointsX = searchPointX.value.split(',');
                var rectPointsY = searchPointY.value.split(',');
                if (isFinite(rectPointsX[0]) && !isNaN(parseFloat(rectPointsX[0])) && isFinite(rectPointsX[2]) && !isNaN(parseFloat(rectPointsX[2])) && isFinite(rectPointsY[0]) && !isNaN(parseFloat(rectPointsY[0])) && isFinite(rectPointsY[2]) && !isNaN(parseFloat(rectPointsY[2]))) {
                    var rectBounds = new mapClient.LatLngBounds();
                    rectBounds.extend(new mapClient.LatLng(rectPointsY[0], rectPointsX[0]));
                    rectBounds.extend(new mapClient.LatLng(rectPointsY[2], rectPointsX[2]));

                    lastDrawnSearchShape = new mapClient.Rectangle(generalOverlayOptions);
                    lastDrawnSearchShape.setBounds(rectBounds);
                    lastDrawnSearchShape.setMap(activeMap);

                    existingSearchShapeBounds = rectBounds;

                    mapClient.event.addListener(lastDrawnSearchShape, 'bounds_changed', rectangleBoundsChangedListener);
                    if (!dontRecord) mapClient.event.trigger(lastDrawnSearchShape, 'bounds_changed');

                    if (listenForBoundsChange) drawingManager.setDrawingMode(mapClient.drawing.OverlayType.RECTANGLE);
                }
            }
        } else if (searchType.value == 'poly') {
            if (searchPointX.value !== '' && searchPointY.value !== '') {
                var pointXArray = searchPointX.value.split(',');
                var pointYArray = searchPointY.value.split(',');
                var usedPointArray = new Array();
                for (var j = 0; j < pointXArray.length; j++) {
                    if (isFinite(pointXArray[j]) && !isNaN(parseFloat(pointXArray[j])) && isFinite(pointYArray[j]) && !isNaN(parseFloat(pointYArray[j]))) {
                        var newPoint = new mapClient.LatLng(pointYArray[j], pointXArray[j]);
                        usedPointArray.push(newPoint);
                        existingSearchShapeBounds.extend(newPoint);
                    }
                }
                if (usedPointArray.length > 0) {
                    lastDrawnSearchShape = new mapClient.Polygon(generalOverlayOptions);
                    lastDrawnSearchShape.setPath(usedPointArray);
                    lastDrawnSearchShape.setMap(activeMap);

                    var polyPath = lastDrawnSearchShape.getPath();
                    mapClient.event.addListener(polyPath, 'insert_at', polyInsertAtListener);
                    mapClient.event.addListener(polyPath, 'remove_at', polyInsertAtListener);
                    mapClient.event.addListener(polyPath, 'set_at', polySetAtListener);
                    if (!dontRecord) polyPath.setAt(0, polyPath.getAt(0));

                    if (listenForBoundsChange) drawingManager.setDrawingMode(mapClient.drawing.OverlayType.POLYGON);
                }
            }
        } else {
            searchDoneRecorder();

            if (listenForBoundsChange) drawingManager.setDrawingMode(null);

            if (typeof (resetReCalcVisibleTimer) != "undefined") resetReCalcVisibleTimer(100);
            else if (typeof (reCalcVisible) != "undefined") reCalcVisible();
        }
    };
    window.refreshDrawnSearch();

    window.updateForm = function (newSearchType, newSearchPointX, newSearchPointY, newSearchCenterX, newSearchCenterY, newSearchRadius, newX1, newX2, newY1, newY2, dontRecord) {
        searchType.value = newSearchType;
        searchCenterX.value = newSearchCenterX;
        searchCenterY.value = newSearchCenterY;
        searchRadius.value = newSearchRadius;
        searchPointX.value = newSearchPointX;
        searchPointY.value = newSearchPointY;
        X1.value = newX1;
        X2.value = newX2;
        Y1.value = newY1;
        Y2.value = newY2;

        window.refreshDrawnSearch(dontRecord);
    };

    var clearActiveSearch = function () {
        window.updateForm('', '', '', '', '', '', '', '', '', '');
    };

    if (listenForBoundsChange) {
        mapClient.event.addListener(activeMap, "bounds_changed", function () {
            //alert(infowindowopen);
            if (!infowindowopen) {
                //alert(drawingManager.getDrawingMode());
                if (drawingManager.getDrawingMode() == null) {
                    clearActiveSearch();
                }

                if (searchType.value == '' || searchType.value == 'bbox') {
                    var currBounds = activeMap.getBounds();
                    var NEPoint = currBounds.getNorthEast();
                    var SWPoint = currBounds.getSouthWest();
                    var newX1 = Math.min(SWPoint.lng(), NEPoint.lng());
                    var newX2 = Math.max(SWPoint.lng(), NEPoint.lng());
                    var newY1 = Math.max(SWPoint.lat(), NEPoint.lat());
                    var newY2 = Math.min(SWPoint.lat(), NEPoint.lat());
                    if (newX1 != X1.value || newX2 != X2.value || newY1 != Y1.value || newY2 != Y2.value) {
                        searchType.value = 'bbox';
                        X1.value = newX1;
                        X2.value = newX2;
                        Y1.value = newY1;
                        Y2.value = newY2;

                        searchDoneRecorder();

                        if (typeof (resetReCalcVisibleTimer) != "undefined") resetReCalcVisibleTimer(2000);
                        else if (typeof (reCalcVisible) != "undefined") reCalcVisible();
                    }
                }
            }
        });
    }

    if (lastDrawnSearchShape) {
        if (!listenForBoundsChange) drawingManager.setDrawingMode(null);
        fitItemsInView(existingSearchShapeBounds);
    }

    window.clearMapSearch = function (resetMapCenter) {
        if (infowindowopen) {
            // close the open window
            infowindowopen = false;
            infowindow.close();
        }
        // turn off the drawing
        drawingManager.setDrawingMode(null);

        // get rid of anything drawn on the map or stored in the form
        clearActiveSearch();

        if (resetMapCenter) {
            activeMap.setCenter(mapCenter);
            activeMap.setZoom(mapZoom);
        }

        if (listenForBoundsChange) {
            // since this map does bbox searching and we just cleared it, we need to make it get added back in
            // (we call this even if the center was reset, because if the we were already at center, the bounds_changed event wouldn't fire
            mapClient.event.trigger(activeMap, "bounds_changed")
        }
    }
}








function mapGetHTTPObject() {
    var xmlhttp = false;
    if (typeof XMLHttpRequest != "undefined") {
        try {
            xmlhttp = new XMLHttpRequest();
            try { xmlhttp.overrideMimeType("text/plain"); } catch (c) { }
        } catch (e) {
            xmlhttp = false;
        }
    }
    if (!xmlhttp && typeof ActiveXObject != "undefined") {
        try {
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (e) {
            try {
                xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
            } catch (c) {
                xmlhttp = false;
            }
        }
    }
    return xmlhttp;
}

if (typeof (updateGeographicFieldsTimer) == "undefined") var updateGeographicFieldsTimer = null;
function clearUpdateGeographicFieldsTimer() {
    if (updateGeographicFieldsTimer != null) {
        clearTimeout(updateGeographicFieldsTimer);
        updateGeographicFieldsTimer = null;
    }
}
function resetGeographicFieldsTimer() {
    clearUpdateGeographicFieldsTimer();
    updateGeographicFieldsTimer = setTimeout('updateGeographicFields();', 500);
}

if (typeof (updateGeographicFieldsActive) == "undefined") var updateGeographicFieldsActive = false;
function cancelUpdateGeographicFieldsAJAX() {
    if (updateGeographicFieldsActive) {
        try { updateGeographicFieldsAJAXObject.onreadystatechange = null; } catch (clearFuncErr) { updateGeographicFieldsAJAXObject.onreadystatechange = function () { }; };
        updateGeographicFieldsAJAXObject.abort();
        updateGeographicFieldsActive = false;
    }
}

if (typeof (updateGeographicFieldsTargetFields) == "undefined") {
    var updateGeographicFieldsXColField, updateGeographicFieldsYColField, updateGeographicFieldsAutoUpdateField, updateGeographicFieldsWMSUrlBase, updateGeographicFieldsVarsToGet;
    var updateGeographicFieldsTargetFields = new Array();
    var updateGeographicFieldsTargetFieldNames = new Array();
    var updateGeographicFieldsTargetValueRegex = new Array();
    var updateGeographicFieldsUnknownText = '';
    var updateGeographicFieldsAJAXObject;
}
function initUpdateGeographicFields(xColFieldName, yColFieldName, updateFieldName, WMSUrl, infoToGet, unknownText) {
    DOMReady.add(function () {
        DOMReadyInitUpdateGeographicFields(xColFieldName, yColFieldName, updateFieldName, WMSUrl, infoToGet, unknownText);
    });
}
function DOMReadyInitUpdateGeographicFields(xColFieldName, yColFieldName, updateFieldName, WMSUrl, infoToGet, unknownText) {
    updateGeographicFieldsAJAXObject = mapGetHTTPObject();

    updateGeographicFieldsXColField = document.getElementByIdOrName(xColFieldName);
    updateGeographicFieldsYColField = document.getElementByIdOrName(yColFieldName);

    updateGeographicFieldsAutoUpdateField = document.getElementByIdOrName(updateFieldName);
    if (!updateGeographicFieldsAutoUpdateField || updateGeographicFieldsAutoUpdateField.type.toLowerCase() != 'checkbox') {
        var allAutoUpdateFields = document.getElementsByName(updateFieldName);
        var foundCheckbox = false;
        for (var i = 0; i < allAutoUpdateFields.length; i++) {
            if (allAutoUpdateFields[i].type.toLowerCase() == 'checkbox') {
                updateGeographicFieldsAutoUpdateField = allAutoUpdateFields[i];
                foundCheckbox = true;
                break;
            }
        }
        if (!foundCheckbox) {
            // no autoUpdate checkbox found, just do it automatically
            updateGeographicFieldsAutoUpdateField = { checked: true };
        }
    }

    updateGeographicFieldsWMSUrlBase = "WMSUrl=" + encodeURIComponent(
        WMSUrl +
        "&SRS=EPSG:4269" +
        "&REQUEST=GetFeatureInfo" +
        "&WIDTH=2&HEIGHT=2" +
        "&X=0&Y=0&INFO_FORMAT=gml&QUERY_LAYERS="
    );
    updateGeographicFieldsVarsToGet = '';

    // info to get should be an array of arrays containing fieldName, layerName, variableName
    var commaNeeded = false;
    for (var i = 0; i < infoToGet.length; i++) {
        if (infoToGet[i] && infoToGet[i][0] != undefined && infoToGet[i][1] != undefined && infoToGet[i][2] != undefined && infoToGet[i][0] != '' && infoToGet[i][1] != '' && infoToGet[i][2] != '') {
            // this array entry has the 3 parts defined
            var currFieldToUpdate = document.getElementByIdOrName(infoToGet[i][0]);
            if (currFieldToUpdate) {
                updateGeographicFieldsTargetFields.push(currFieldToUpdate);
                updateGeographicFieldsTargetFieldNames.push(infoToGet[i][0]);
                updateGeographicFieldsTargetValueRegex.push((infoToGet[i][3] != undefined ? infoToGet[i][3] : ''));
                updateGeographicFieldsWMSUrlBase += encodeURIComponent(
                    (commaNeeded ? ',' : '') +
                    infoToGet[i][1]
                );
                updateGeographicFieldsVarsToGet += "&VarToGet[]=" + encodeURIComponent(
                    infoToGet[i][2]
                );
                commaNeeded = true;
            }
        }
    }

    if (unknownText != undefined && unknownText != null) {
        updateGeographicFieldsUnknownText = unknownText;
    }
}

// make sure the init is called first, currently only works with single select lists or text boxes, would need to update to support other types
function updateGeographicFields() {
    clearUpdateGeographicFieldsTimer();
    cancelUpdateGeographicFieldsAJAX();
    console.log(updateGeographicFieldsWMSUrlBase);
    if (updateGeographicFieldsAutoUpdateField.checked) {
        var lng, lat;
        if (isFinite(updateGeographicFieldsXColField.value) && !isNaN(lng = parseFloat(updateGeographicFieldsXColField.value)) && isFinite(updateGeographicFieldsYColField.value) && !isNaN(lat = parseFloat(updateGeographicFieldsYColField.value))) {
            updateGeographicFieldsAJAXObject.open("POST", "http://projects.inweh.unu.edu/hydrosanitas/WIDEMaps/WIDEMapsWMSFeatureQuery.php", true);
            updateGeographicFieldsAJAXObject.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            updateGeographicFieldsAJAXObject.onreadystatechange = function () { handleUpdateGeographicFieldsResponse(); };

            updateGeographicFieldsActive = true;
            
            updateGeographicFieldsAJAXObject.send(
                updateGeographicFieldsWMSUrlBase +
                encodeURIComponent("&BBox=" + (lng) + "," + (lat - 0.0000001) + "," + (lng + 0.0000001) + "," + (lat)) +
                updateGeographicFieldsVarsToGet
            );
        }
    }
}


function handleUpdateGeographicFieldsResponse() {
    if (updateGeographicFieldsAJAXObject.readyState == 4 && updateGeographicFieldsAJAXObject.status == 200 && updateGeographicFieldsAJAXObject.responseText != "") {
        var fieldValuesSet = new Array();

        // parse all of the values received from the server
        var fieldValues = updateGeographicFieldsAJAXObject.responseText.split("\n");
        for (var fieldKey = 0; fieldKey < fieldValues.length; fieldKey++) {
            var currFieldName = updateGeographicFieldsTargetFieldNames[fieldKey];
            if (!fieldValuesSet[currFieldName]) {
                var currField = updateGeographicFieldsTargetFields[fieldKey];
                var currFieldValue = fieldValues[fieldKey];
                var currFieldRegex = updateGeographicFieldsTargetValueRegex[fieldKey];
                if (currFieldRegex != '') {
                    var regexMatch = currFieldValue.match(new RegExp(currFieldRegex, "i"));
                    if (regexMatch != null) {
                        currFieldValue = (regexMatch[1] != undefined ? regexMatch[1] : regexMatch[0]);
                    } else {
                        currFieldValue = '';
                    }
                }
                currFieldValue = currFieldValue.replace(/^\s\s*/, "").replace(/\s*\s$/, "");

                if (currField.type == "select" || currField.type == "select-one") {
                    currField.selectedIndex = 0;
                    for (var listKey = 0; listKey < currField.options.length; listKey++) {
                        if (currField.options[listKey].text.replace(/^\s\s*/, "").replace(/\s*\s$/, "") == currFieldValue) {
                            currField.selectedIndex = listKey;
                            if (currFieldValue != '') {
                                fieldValuesSet[currFieldName] = true;
                            }
                            break;
                        }
                    }
                    if (!fieldValuesSet[currFieldName]) {
                        for (var listKey = 0; listKey < currField.options.length; listKey++) {
                            if (currField.options[listKey].value.replace(/^\s\s*/, "").replace(/\s*\s$/, "") == currFieldValue) {
                                currField.selectedIndex = listKey;
                                if (currFieldValue != '') {
                                    fieldValuesSet[currFieldName] = true;
                                }
                                break;
                            }
                        }
                    }
                } else {
                    if (currFieldValue != '') {
                        fieldValuesSet[currFieldName] = true;
                    } else {
                        currFieldValue = updateGeographicFieldsUnknownText;
                    }
                    currField.value = currFieldValue;
                }
            }
        }
    }
}

function createCommonMapVars(varPrefix, objVars, imgVars) {
    var commonVars = new Array('UTMs', 'Labels', 'Links', 'Classes', 'Styles', 'CoordTypes', 'Descriptions');
    if (objVars) commonVars.push('Radii');
    if (imgVars) commonVars = commonVars.concat(new Array('Files', 'Widths', 'Heights'));
    for (var i = 0; i < commonVars.length; i++) {
        window[varPrefix + commonVars[i]] = new Array();
    }
}

function addToCommonMapVars(varPrefix, coords, label, link, theClass, coordType, theStyle, descr) {
    window[varPrefix + 'UTMs'].push(coords);
    window[varPrefix + 'Labels'].push(label);
    window[varPrefix + 'Links'].push(link);
    window[varPrefix + 'Classes'].push(theClass || '');
    window[varPrefix + 'Styles'].push(theStyle || '');
    window[varPrefix + 'CoordTypes'].push(coordType || '');
    window[varPrefix + 'Descriptions'].push(descr || '');
}
function addToObjectMapVars(varPrefix, coords, label, link, theClass, coordType, theStyle, radius, descr) {
    addToCommonMapVars(varPrefix, coords, label, link, theClass, coordType, theStyle, descr);
    window[varPrefix + 'Radii'].push((radius != undefined ? radius : ''));
}
function addToImageMapVars(varPrefix, coords, file, width, height, label, link, theClass, coordType, theStyle, descr) {
    addToCommonMapVars(varPrefix, coords, label, link, theClass, coordType, theStyle, descr);
    window[varPrefix + 'Files'].push(file);
    window[varPrefix + 'Widths'].push(width);
    window[varPrefix + 'Heights'].push(height);
}

if (typeof (mappedAreaUTMs) == 'undefined') {
    createCommonMapVars('mappedArea');
    window.addAreaToMap = function (coords, label, link, theClass, coordType, theStyle, descr) {
        addToCommonMapVars('mappedArea', coords, label, link, theClass, coordType, theStyle, descr);
    }
}
if (typeof (mappedObjectUTMs) == 'undefined') {
    createCommonMapVars('mappedObject', true);
    window.addObjectToMap = function (coords, label, link, theClass, coordType, theStyle, radius, descr) {
        addToObjectMapVars('mappedObject', coords, label, link, theClass, coordType, theStyle, radius, descr);
    }
}
if (typeof (mappedImageUTMs) == 'undefined') {
    createCommonMapVars('mappedImage', false, true);
    window.addImageToMap = function (coords, file, width, height, label, link, theClass, coordType, theStyle, descr) {
        addToImageMapVars('mappedImage', coords, file, width, height, label, link, theClass, coordType, theStyle, descr);
    }
}

if (typeof (mappedAreaSpecificUTMs) == 'undefined') {
    createCommonMapVars('mappedAreaSpecific');
    window.addAreaSpecificToMap = function (coords, label, link, theClass, coordType, theStyle, descr) {
        addToCommonMapVars('mappedAreaSpecific', coords, label, link, theClass, coordType, theStyle, descr);
    }
    window.resetAreaSpecifics = function () {
        createCommonMapVars('mappedAreaSpecific');
    }
}
if (typeof (mappedObjectSpecificUTMs) == 'undefined') {
    createCommonMapVars('mappedObjectSpecific', true);
    window.addObjectSpecificToMap = function (coords, label, link, theClass, coordType, theStyle, radius, descr) {
        addToObjectMapVars('mappedObjectSpecific', coords, label, link, theClass, coordType, theStyle, radius, descr);
    }
    window.resetObjectSpecifics = function () {
        createCommonMapVars('mappedObjectSpecific', true);
    }
}
if (typeof (mappedImageSpecificUTMs) == 'undefined') {
    createCommonMapVars('mappedImageSpecific', false, true);
    window.addImageSpecificToMap = function (coords, file, width, height, label, link, theClass, coordType, theStyle, descr) {
        addToImageMapVars('mappedImageSpecific', coords, file, width, height, label, link, theClass, coordType, theStyle, descr);
    }
    window.resetImageSpecifics = function () {
        createCommonMapVars('mappedImageSpecific', false, true);
    }
}

function calculateDrawnSize(lastDrawnShape, lastDrawnType) {
    var siteAreaObject = document.getElementByIdOrName("SiteArea");
    var siteAreaCalcObject = document.getElementByIdOrName("SiteAreaCalc");
    var sitePerimeterObject = document.getElementByIdOrName("SitePerimeter");
    var sitePerimeterCalcObject = document.getElementByIdOrName("SitePerimeterCalc");

    if (!siteAreaCalcObject || siteAreaCalcObject.type.toLowerCase() != 'checkbox') {
        siteAreaCalcObject = null;
        var allAutoCalcFields = document.getElementsByName("SiteAreaCalc");
        for (var i = 0; i < allAutoCalcFields.length; i++) {
            if (allAutoCalcFields[i].type.toLowerCase() == 'checkbox') {
                siteAreaCalcObject = allAutoCalcFields[i];
                break;
            }
        }
    }
    if (!sitePerimeterCalcObject || sitePerimeterCalcObject.type.toLowerCase() != 'checkbox') {
        sitePerimeterCalcObject = null;
        var allAutoCalcFields = document.getElementsByName("SitePerimeterCalc");
        for (var i = 0; i < allAutoCalcFields.length; i++) {
            if (allAutoCalcFields[i].type.toLowerCase() == 'checkbox') {
                sitePerimeterCalcObject = allAutoCalcFields[i];
                break;
            }
        }
    }

    if ((siteAreaObject && (!siteAreaCalcObject || siteAreaCalcObject.checked)) || (sitePerimeterObject && (!sitePerimeterCalcObject || sitePerimeterCalcObject.checked))) {
        if (lastDrawnType == 'circle') {
            var currRadius = lastDrawnShape.getRadius();
            if (siteAreaObject && (!siteAreaCalcObject || siteAreaCalcObject.checked)) siteAreaObject.value = (currRadius * currRadius * WIDEMaps.Coords.PI).toFixed(2);
            if (sitePerimeterObject && (!sitePerimeterCalcObject || sitePerimeterCalcObject.checked)) sitePerimeterObject.value = (currRadius * WIDEMaps.Coords.TWO_PI).toFixed(2);
        } else if (lastDrawnType == 'polygon' || lastDrawnType == 'polyline') {
            var currPath = lastDrawnShape.getArray(); // it's already the path
            if (lastDrawnType == 'polygon') {
                if (siteAreaObject && (!siteAreaCalcObject || siteAreaCalcObject.checked)) siteAreaObject.value = map.metresAreaBetweenPoints(currPath).toFixed(2);
            } else {
                if (siteAreaObject && (!siteAreaCalcObject || siteAreaCalcObject.checked)) siteAreaObject.value = '';
            }
            if (sitePerimeterObject && (!sitePerimeterCalcObject || sitePerimeterCalcObject.checked)) sitePerimeterObject.value = map.metresBetweenPoints(currPath).toFixed(2);
        } else {
            if (siteAreaObject && (!siteAreaCalcObject || siteAreaCalcObject.checked)) siteAreaObject.value = '';
            if (sitePerimeterObject && (!sitePerimeterCalcObject || sitePerimeterCalcObject.checked)) sitePerimeterObject.value = '';
        }
    }
}