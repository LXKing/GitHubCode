var edit = function (editor, e) {
    /*
        "e" is an edit event with the following properties:

            grid - The grid
            record - The record that was edited
            field - The field name that was edited
            value - The value being set
            originalValue - The original value for the field, before the edit.
            row - The grid table row
            column - The grid Column defining the column that was edited.
            rowIdx - The row index that was edited
            colIdx - The column index that was edited
    */

    // Call DirectMethod
    if (!(e.value === e.originalValue || (Ext.isDate(e.value) && Ext.Date.isEqual(e.value, e.originalValue)))) {
        CompanyX.Edit(e.record.data.ID, e.field, e.originalValue, e.value, e.record.data);
    }
};