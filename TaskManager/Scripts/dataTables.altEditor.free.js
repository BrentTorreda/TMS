/*! Datatables altEditor 1.0
 */

/**
 * @summary     altEditor
 * @description Lightweight editor for DataTables
 * @version     1.0
 * @file        dataTables.editor.lite.js
 * @author      kingkode (www.kingkode.com)
 * @contact     www.kingkode.com/contact
 * @copyright   Copyright 2016 Kingkode
 *
 * This source file is free software, available under the following license:
 *   MIT license - http://datatables.net/license/mit
 *
 * This source file is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE. See the license files for details.
 *
 * For details please refer to: http://www.kingkode.com
 */

(function (factory) {
    if (typeof define === 'function' && define.amd) {
        // AMD
        define(['jquery', 'datatables.net'], function ($) {
            return factory($, window, document);
        });
    }
    else if (typeof exports === 'object') {
        // CommonJS
        module.exports = function (root, $) {
            if (!root) {
                root = window;
            }

            if (!$ || !$.fn.dataTable) {
                $ = require('datatables.net')(root, $).$;
            }

            return factory($, root, root.document);
        };
    }
    else {
        // Browser
        factory(jQuery, window, document);
    }
}(function ($, window, document, undefined) {
    'use strict';
    var DataTable = $.fn.dataTable;

    var _instance = 0;

    /** 
     * altEditor provides modal editing of records for Datatables
     *
     * @class altEditor
     * @constructor
     * @param {object} dt DataTables settings object
     * @param {object} opts Configuration object for altEditor
     */
    var altEditor = function (dt, opts) {
        if (!DataTable.versionCheck || !DataTable.versionCheck('1.10.8')) {
            throw ("Warning: altEditor requires DataTables 1.10.8 or greater");
        }

        // User and defaults configuration object
        this.c = $.extend(true, {},
            DataTable.defaults.altEditor,
            altEditor.defaults,
            opts
        );

        /**
         * @namespace Settings object which contains customisable information for altEditor instance
         */
        this.s = {
            /** @type {DataTable.Api} DataTables' API instance */
            dt: new DataTable.Api(dt),

            /** @type {String} Unique namespace for events attached to the document */
            namespace: '.altEditor' + (_instance++)
        };


        /**
         * @namespace Common and useful DOM elements for the class instance
         */
        this.dom = {
            /** @type {jQuery} altEditor handle */
            modal: $('<div class="dt-altEditor-handle"/>'),
        };


        /* Constructor logic */
        this._constructor();
    }



    $.extend(altEditor.prototype, {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
         * Constructor
         * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /**
         * Initialise the RowReorder instance
         *
         * @private
         */
        _constructor: function () {
            // console.log('altEditor Enabled')
            var that = this;
            var dt = this.s.dt;

            this._setup();

            dt.on('destroy.altEditor', function () {
                dt.off('.altEditor');
                $(dt.table().body()).off(that.s.namespace);
                $(document.body).off(that.s.namespace);
            });
        },

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
         * Private methods
         * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /**
         * Setup dom and bind button actions
         *
         * @private
         */
        _setup: function () {
            // console.log('Setup');

            var that = this;
            var dt = this.s.dt;

            // add modal
            $('body').append('\
            <div class="modal fade" id="altEditor-modal" tabindex="-1" role="dialog">\
              <div class="modal-dialog">\
                <div class="modal-content">\
                  <div class="modal-header">\
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>\
                    <h4 class="modal-title"></h4>\
                  </div>\
                  <div class="modal-body">\
                    <p></p>\
                  </div>\
                  <div class="modal-footer">\
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>\
                    <button type="button" class="btn btn-primary">Save changes</button>\
                  </div>\
                </div>\
              </div>\
            </div>'
            );


            // add Edit Button
            if (this.s.dt.button('edit:name')) {
                this.s.dt.button('edit:name').action(function (e, dt, node, config) {
                    var rows = dt.rows({
                        selected: true
                    }).count();

                    that._openEditModal();
                });

                $(document).on('click', '#editRowBtn', function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    that._editRowData();
                });
            }

            // add Delete Button
            if (this.s.dt.button('delete:name')) {
                this.s.dt.button('delete:name').action(function (e, dt, node, config) {
                    var rows = dt.rows({
                        selected: true
                    }).count();

                    that._openDeleteModal();
                });

                $(document).on('click', '#deleteRowBtn', function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    that._deleteRow();
                });
            }

            // add Add Button
            if (this.s.dt.button('add:name')) {
                this.s.dt.button('add:name').action(function (e, dt, node, config) {
                    var rows = dt.rows({
                        selected: true
                    }).count();

                    that._openAddModal();
                });

                $(document).on('click', '#addRowBtn', function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    that._addRowData();
                });
            }

        },

        /**
         * Emit an event on the DataTable for listeners
         *
         * @param  {string} name Event name
         * @param  {array} args Event arguments
         * @private
         */
        _emitEvent: function (name, args) {
            this.s.dt.iterator('table', function (ctx, i) {
                $(ctx.nTable).triggerHandler(name + '.dt', args);
            });
        },

        /**
         * Open Edit Modal for selected row
         * 
         * @private
         */
        _openEditModal: function () {
            var that = this;
            var dt = this.s.dt;

            var columnDefs = [];

            for (var i = 0; i < dt.context[0].aoColumns.length; i++) {
                columnDefs.push({ title: dt.context[0].aoColumns[i].sTitle })
            }
            
            var adata = dt.rows({ selected: true }).data();

            var data = "";

            data += "<form id='altEditor-form' name='altEditor-form' role='form' >";

            //Only one row at a time.
            for (var j = 0; j <= adata.length; j++) { /*Assume 0 is the ID*/
                if (j === 0) {
                    data += "<input type='hidden' id='" + columnDefs[j].title + "' name='" + columnDefs[j].title + "' placeholder='" + columnDefs[j].title + "' value='" + adata[0][columnDefs[j].title] + "' />";
                }
                else {
                    data += "<div class='form-group'><div class='col-sm-3 col-md-3 col-lg-3 text-right' style='padding-top:7px;'><label for='" + columnDefs[j].title + "'>" + columnDefs[j].title + ":</label></div><div class='col-sm-9 col-md-9 col-lg-9'><input type='text'  id='" + columnDefs[j].title + "' name='" + columnDefs[j].title + "' placeholder='" + columnDefs[j].title + "' style='overflow:hidden'  class='form-control  form-control-sm' value='" + adata[0][columnDefs[j].title] + "'></div><div style='clear:both;'></div></div>";
                }
            }
            data += "</form>";


            $('#altEditor-modal').on('show.bs.modal', function () {
                $('#altEditor-modal').find('.modal-title').html('Edit Record');
                $('#altEditor-modal').find('.modal-body').html('<pre>' + data + '</pre>');
                $('#altEditor-modal').find('.modal-footer').html("<button type='button' data-content='remove' class='btn btn-default' data-dismiss='modal'>Close</button>\
               <button type='button' data-content='remove' class='btn btn-primary' id='editRowBtn'>Save Changes</button>");
            });

            $('#altEditor-modal').modal('show');
            $('#altEditor-modal input[0]').focus();

        },

        _editRowData: function () {
            var that = this;
            var dt = this.s.dt;

            var data = [];

            $('form[name="altEditor-form"] input').each(function (i) {
                data.push($(this).val());
            });

            var api = $('#apiName').val();
            var rowId = data[0]; //Assume element 0 is the ID

            updateDb("altEditor-form", api, rowId, dt, "POST");

            $('#altEditor-modal .modal-body .alert').remove();

            var message = '<div class="alert alert-success" role="alert">\
             <strong>Success!</strong> This record has been updated.\
           </div>';

            $('#altEditor-modal .modal-body').append(message);
        },


        /**
         * Open Delete Modal for selected row
         *
         * @private
         */
        _openDeleteModal: function () {
            var that = this;
            var dt = this.s.dt;

            var columnDefs = [];

            for (var i = 0; i < dt.context[0].aoColumns.length; i++) {
                columnDefs.push({ title: dt.context[0].aoColumns[i].sTitle })
            }

            var adata = dt.rows({ selected: true }).data(); //BTo - Added .data() since it seems to return undefined otherwise.

            var data = "";

            data += "<form id='altEditor-form' name='altEditor-form' role='form'>";

            //BTo - Assume element 0 is ID. Only 1 row at a time, no multi row deletes.
            for (var j = 0; j <= adata.length; j++) {
                if (j === 0) {
                    data += "<input type='hidden' id='" + columnDefs[j].title + "' name='" + columnDefs[j].title + "' placeholder='" + columnDefs[j].title + "' value='" + adata[0][columnDefs[j].title] + "' />";
                }
                else {
                    data += "<div class='form-group'><div class='col-sm-3 col-md-3 col-lg-3 text-right' style='padding-top:7px;'><label for='" + columnDefs[j].title + "'>" + columnDefs[j].title + ":</label></div><div class='col-sm-9 col-md-9 col-lg-9'><input type='text'  id='" + columnDefs[j].title + "' name='" + columnDefs[j].title + "' placeholder='" + columnDefs[j].title + "' style='overflow:hidden'  class='form-control  form-control-sm' value='" + adata[0][columnDefs[j].title] + "'></div><div style='clear:both;'></div></div>";
                }
            }
            data += "</form>";

            $('#altEditor-modal').on('show.bs.modal', function () {
                $('#altEditor-modal').find('.modal-title').html('Delete Record');
                $('#altEditor-modal').find('.modal-body').html('<pre>' + data + '</pre>');
                $('#altEditor-modal').find('.modal-footer').html("<button type='button' data-content='remove' class='btn btn-default' data-dismiss='modal'>Close</button>\
               <button type='button' data-content='remove' class='btn btn-danger' id='deleteRowBtn'>Delete</button>");
            });

            $('#altEditor-modal').modal('show');
            $('#altEditor-modal input[0]').focus();

        },

        _deleteRow: function () {
            var that = this;
            var dt = this.s.dt;

            var data = [];

            $('form[name="altEditor-form"] input').each(function (i) {
                data.push($(this).val());
            });

            var api = $('#apiName').val();
            var rowId = data[0]; //BTo - Assume element 0 is the ID

            updateDb("altEditor-form", api, rowId, dt, "DELETE");

            $('#altEditor-modal .modal-body .alert').remove();

            var message = '<div class="alert alert-success" role="alert">\
           <strong>Success!</strong> This record has been deleted.\
         </div>';

            $('#altEditor-modal .modal-body').append(message);
        },

        /**
         * Open Add Modal for selected row
         * 
         * @private
         */
        _openAddModal: function () {
            var that = this;
            var dt = this.s.dt;

            var columnDefs = [];

            for (var i = 0; i < dt.context[0].aoColumns.length; i++) {
                columnDefs.push({ title: dt.context[0].aoColumns[i].sTitle })
            }

            var data = "";

            data += "<form id='altEditor-form' name='altEditor-form' role='form'>";

            for (var j = 0; j < dt.context[0].aoColumns.length; j++) {
                if (j === 0) {
                    data += "<input type='hidden' id='" + columnDefs[j].title + "' name='" + columnDefs[j].title + "' placeholder='" + columnDefs[j].title + "' />";
                }
                else {
                    data += "<div class='form-group'><div class='col-sm-3 col-md-3 col-lg-3 text-right' style='padding-top:7px;'><label for='" + columnDefs[j].title + "'>" + columnDefs[j].title + ":</label></div><div class='col-sm-9 col-md-9 col-lg-9'><input type='text'  id='" + columnDefs[j].title + "' name='" + columnDefs[j].title + "' placeholder='" + columnDefs[j].title + "' style='overflow:hidden'  class='form-control  form-control-sm' value=''></div><div style='clear:both;'></div></div>";
                }                
            }
            data += "</form>";

            $('#altEditor-modal').on('show.bs.modal', function () {
                $('#altEditor-modal').find('.modal-title').html('Add Record');
                $('#altEditor-modal').find('.modal-body').html('<pre>' + data + '</pre>');
                $('#altEditor-modal').find('.modal-footer').html("<button type='button' data-content='remove' class='btn btn-default' data-dismiss='modal'>Close</button>\
               <button type='button' data-content='remove' class='btn btn-primary' id='addRowBtn'>Add Record</button>");
            });

            $('#altEditor-modal').modal('show');
            $('#altEditor-modal input[0]').focus();
        },

        _addRowData: function () {
            console.log('add row')
            var that = this;
            var dt = this.s.dt;

            var data = [];

            $('form[name="altEditor-form"] input').each(function (i) {
                data.push($(this).val());
            });

            var api = $('#apiName').val();
            var rowId = 0;

            updateDb("altEditor-form", api, rowId, dt, "POST");

            $('#altEditor-modal .modal-body .alert').remove();

            var message = '<div class="alert alert-success" role="alert">\
            <strong>Success!</strong> This record has been added.\
            </div>';

            $('#altEditor-modal .modal-body').append(message);
        },

        _getExecutionLocationFolder: function () {
            var fileName = "dataTables.altEditor.js";
            var scriptList = $("script[src]");
            var jsFileObject = $.grep(scriptList, function (el) {

                if (el.src.indexOf(fileName) !== -1) {
                    return el;
                }
            });
            var jsFilePath = jsFileObject[0].src;
            var jsFileDirectory = jsFilePath.substring(0, jsFilePath.lastIndexOf("/") + 1);
            return jsFileDirectory;
        }
    });

    //BTo - 10/02/18 - For add, edit and delete
    function updateDb (formName, table, recId, dt, apiType) {
        var myform = document.getElementById(formName);
        var fd = new FormData(myform);

        $.ajax({
            url: "/api/" + table + "/" + recId,
            data: fd,
            cache: false,
            processData: false,
            contentType: false,
            type: apiType,
            success: function () {
                toastr.success("Changes successful.");

                //BTo - refetch the ajax data
                dt.ajax.reload();
                dt.draw();

                //BTo - disable the add/edit/delete button
                if (apiType === "POST" && recId === 0) {
                    $("#addRowBtn").prop('disabled', true);
                } else if (apiType === "POST" && recId > 0) {
                    $("#editRowBtn").prop('disabled', true);
                } else {
                    $("#deleteRowBtn").prop('disabled', true);
                }            
            },
            error: function () {
                toastr.error("Something went wrong.");
            }
        });            
    };


    /**
     * altEditor version
     * 
     * @static
     * @type      String
     */
    altEditor.version = '1.0';


    /**
     * altEditor defaults
     * 
     * @namespace
     */
    altEditor.defaults = {
        /** @type {Boolean} Ask user what they want to do, even for a single option */
        alwaysAsk: false,

        /** @type {string|null} What will trigger a focus */
        focus: null, // focus, click, hover

        /** @type {column-selector} Columns to provide auto fill for */
        columns: '', // all

        /** @type {boolean|null} Update the cells after a drag */
        update: null, // false is editor given, true otherwise

        /** @type {DataTable.Editor} Editor instance for automatic submission */
        editor: null
    };


    /**
     * Classes used by altEditor that are configurable
     * 
     * @namespace
     */
    altEditor.classes = {
        /** @type {String} Class used by the selection button */
        btn: 'btn'
    };


    // Attach a listener to the document which listens for DataTables initialisation
    // events so we can automatically initialise
    $(document).on('preInit.dt.altEditor', function (e, settings, json) {
        if (e.namespace !== 'dt') {
            return;
        }

        var init = settings.oInit.altEditor;
        var defaults = DataTable.defaults.altEditor;

        if (init || defaults) {
            var opts = $.extend({}, init, defaults);

            if (init !== false) {
                new altEditor(settings, opts);
            }
        }
    });


    // Alias for access
    DataTable.altEditor = altEditor;

    return altEditor;
}));