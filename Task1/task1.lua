function filtered_sum_robust(value, stepper_function, filter_function, cancel_function)
    return (
        filter_function(value) and value or 0
    ) + (
        not cancel_function(stepper_function(value)) and filtered_sum_robust(stepper_function(value), stepper_function, filter_function, cancel_function) or 0
    )
end


function FinnMultipler(AlleTallLavereEnn)
    return filtered_sum_robust(
        1,
        function(x) return x + 1 end,
        function(x) return (x % 3 == 0) or (x % 5 == 0) end,
        function(x) return x >= AlleTallLavereEnn end
    )
end

assert(FinnMultipler(10) == 23, "function did not satisfy the one example provided in the task, how embarrasing")
assert(FinnMultipler(1000) == 233168, "function did not satisfy the one example provided in the task, how embarrasing")
assert(FinnMultipler(0) == 0, "The sum of zero numbers is 0")
assert(FinnMultipler(1) == 0, "There are no mulpitples of 3 or 5 that are <= 2, thus the result should be 0")
assert(FinnMultipler(11) == 33, "FinnMultipler(11) should be 23 + 10 = 33")
