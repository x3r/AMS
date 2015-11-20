$(function () {
    var dates = $("input[name=Dates]").val().length == 0 ? [] : $("input[name=Dates]").val().split(',');
    var i = 0;
    $("#datePicker").multiDatesPicker(
    {
        onSelect:
            function (date) {
                var pushable = true;
                for (i = 0; i < dates.length; i++) {
                    if (dates[i] == date) {
                        dates.splice(i, 1);
                        pushable = false;
                    }
                }
                if (pushable) {
                    dates.push(date);
                }
                $
                ("input[name=Dates]").val(dates.toString());
            },
        addDates: $("input[name=Dates]").val().length == 0 ? null : $("input[name=Dates]").val().split(',')
    });
});
//($
//            ("input[name=Dates]").val() == null || $
//            ("input[name=Dates]").val().length <= 0) ? null :
//        function () {
//            var val = $
//            ("input[name=Dates]").val();
//            if (val !== null && val.length > 0) {
//                var available = val.split(',');
//                if (available.length == 0) {
//                    available.append(val);
//                }
//                return available;
//            }
//        }