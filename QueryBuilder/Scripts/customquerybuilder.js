$('#builder').queryBuilder({

    sortable: true,

    filters: [{
        id: 'core_ID',
        type: 'integer',
        operators: ['equal', 'not_equal', 'in', 'not_in']
    }, {
        id: 'store_id',
        label: 'Store ID',
        type: 'string',
        operators: ['equal', 'not_equal', 'in', 'not_in']
    }]

});


// set rules
$('#builder').queryBuilder('setRules', {
    "condition": "AND",
    "rules": [
        {
            "id": "core_ID",
            "field": "core_ID",
            "type": "integer",
            "input": "text",
            "operator": "in",
            "value": "1240"
        }
    ]
});


// reset builder
$('.reset').on('click', function () {

    $('#builder').queryBuilder('reset');

    $(".json-parsed").empty();

    $(".sql-parsed").empty();

});

// get rules & SQL
$('.parse-sql').on('click', function () {

    // JSON

    var resJson = $('#builder').queryBuilder('getRules');

    $(".json-parsed").html(JSON.stringify(resJson, null, 2));

    // SQL

    var resSql = $('#builder').queryBuilder('getSQL', false);

    $(".sql-parsed").html(resSql.sql);

});

// result

$(document).ready(function () {

    $(".parse-sql").trigger("click");

});