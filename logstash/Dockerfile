FROM docker.elastic.co/logstash/logstash-oss:6.3.2

# Add your logstash plugins setup here
# Example: RUN logstash-plugin install logstash-filter-json
RUN logstash-plugin install logstash-filter-alter
RUN logstash-plugin install logstash-filter-prune
# COPY logstash-output-loganalytics-0.1.0.gem /tmp
# RUN logstash-plugin install /tmp/logstash-output-loganalytics-0.1.0.gem
RUN logstash-plugin install --version '0.2.0' logstash-output-loganalytics
RUN logstash-plugin install logstash-output-applicationinsights

COPY config/logstash.yml /usr/share/logstash/config
COPY config/pipelines.yml /usr/share/logstash/config
COPY pipeline/logs.conf /usr/share/logstash/pipeline
COPY pipeline/metrics.conf /usr/share/logstash/pipeline
