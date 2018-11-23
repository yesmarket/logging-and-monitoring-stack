# https://github.com/elastic/logstash-docker
FROM docker.elastic.co/logstash/logstash-oss:6.3.2

# Add your logstash plugins setup here
# Example: RUN logstash-plugin install logstash-filter-json
RUN logstash-plugin install logstash-filter-alter
RUN logstash-plugin install logstash-filter-prune
RUN logstash-plugin install logstash-output-azure_loganalytics

COPY config/logstash.yml /usr/share/logstash/config
COPY config/pipelines.yml /usr/share/logstash/config
COPY pipeline/logs.conf /usr/share/logstash/pipeline
COPY pipeline/metrics.conf /usr/share/logstash/pipeline