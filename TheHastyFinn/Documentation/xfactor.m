% netflix xfactor test
clear all;
close all;

stock = 'NSSC';
periods = [50 75 100 125];

[hist_date, hist_high, hist_low, hist_open, hist_close, hist_vol] = get_hist_stock_data(stock);

times = datenum(hist_date);

% velocity (green) = ((numperiods - periodssincehighesthigh) / numperiods) * 100
% gravity (red) = ((numperiods - periodssincelowestlow) / numperiods) * 100

%period = 120; % days? 50 or 200 are pretty common

% tracks high/low
highesthigh = 0;
lowestlow = 100000000;
% tracking days since high/low
periodssincehighesthigh = 0;
periodssincelowestlow = 0;
% when the high/low happened
highesthighperiod = 0;
lowestlowperiod = 0;

% moving window of the period
% highest high means number of days since the window's high
% i = the day?

count = length(times);
countperiods = length(periods);
for j=1:countperiods
    
    % reset!
    highesthigh = 0;
    lowestlow = 100000000;
    periodssincehighesthigh = 0;
    periodssincelowestlow = 0;
    highesthighperiod = 0;
    lowestlowperiod = 0;
    
    period = periods(j);
    for i=1:count

        if i > period
            windowpoint = i-period;
        else
            windowpoint = 1;
        end

        % SEGMENTS ARE ONE TOO LONG!!
        segment = hist_close(windowpoint:i);
        %segment = data(windowpoint:i-1,4);

        if i == 34
        end
        % THIS CAN EXPIRE!! must account for that
        % find highest high for period
        if highesthighperiod < (i-period)
            highesthigh = 0;
        end;
        if max(segment) > highesthigh
            [highesthigh, highidx] = max(segment);
            highesthighperiod = highidx + (i-period);
        end

        % find lowest low for period
        if lowestlowperiod < (i-period)
            lowestlow = 10000000;
        end
        if min(segment) < lowestlow
            [lowestlow, lowidx] = min(segment);
            lowestlowperiod = lowidx + (i-period);
        end

        % calc num of periods since high/low
        periodssincehighesthigh = i - highesthighperiod;
        periodssincelowestlow = i - lowestlowperiod;

        % calc velocity
        velocity(i,j) = ((period - periodssincehighesthigh) / period) * 100;
        if velocity(i,j) > 100
        end

        % calc gravity
        gravity(i,j) = ((period - periodssincelowestlow) / period ) * 100;
        if gravity(i,j) > 100
        end
    end
end

figure(10);
movegui('east');

subplot(countperiods+1,1,1);
plot(times,hist_close);
grid; datetick;
title(stock);
xlim([735600 times(end)]); % 735600 = 2014/01/01

for k=1:countperiods
    subplot(countperiods+1,1,k+1);
    hold on;
    plot(times,velocity(:,k),'g');
    plot(times,gravity(:,k),'r');
    hold off;
    
    grid; datetick;
    title(sprintf('%d Days',periods(k)));
    xlim([735600 times(end)]); % 735600 = 2014/01/01
    ylim([0 100]);
end